using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PoDato {

	internal sealed partial class Reader : IReader {

		internal delegate TVal ReadFunc<TVal>(Tater tater);

		private Lexer m_lexer;
		private Parser m_parser;

		private Stack<Tater> m_input;
		private List<ReadError> m_errors;
		private List<string> m_context;
		private StringBuilder m_builder;

		private bool m_doPopOnLog = false;

		private Tater Current { get { return m_input.Peek(); } }

		public Reader(int tabSize = 4) {
			m_lexer = new Lexer(tabSize);
			m_parser = new Parser();
			m_input = new Stack<Tater>();
			m_errors = new List<ReadError>();
			m_context = new List<string>();
			m_builder = new StringBuilder();
		}

		public ReadResult<T> Read<T>(string input) where T : IReadable, new() {
			m_errors.Clear();
			try {
				Tater tater = m_parser.Parse(m_lexer.Tokenize(input));
				ReadResult<T> result;
				if (tater.IsArray) {
					result = ReadTaterArray<T>(tater);
				} else {
					result = ReadTaterObject<T>(tater);
				}
				return result;
			} catch (ParseException e) {
				LogError(e);
				T value = default;
				return new ReadResult<T>(value, m_errors);
			}
		}
		public ReadResult<T> Read<T>(Tater input) where T : IReadable, new() {
			m_errors.Clear();
			try {
				ReadResult<T> result;
				if (input.IsArray) {
					result = ReadTaterArray<T>(input);
				} else if (input.IsObject) {
					result = ReadTaterObject<T>(input);
				} else {
					throw new Exception("Input Tater must be an Array or Object");
				}
				return result;
			} catch (ParseException e) {
				LogError(e);
				T value = default;
				return new ReadResult<T>(value, m_errors);
			}
		}

		public ReadResult Read(string input) {
			m_errors.Clear();
			try {
				Tater tater = m_parser.Parse(m_lexer.Tokenize(input));
				return new ReadResult(tater, m_errors);
			} catch (ParseException e) {
				LogError(e);
				return new ReadResult(null, m_errors);
			}
		}
		private ReadResult<T> ReadTaterObject<T>(Tater input) where T : IReadable, new() {
			m_input.Clear();
			m_context.Clear();
			Push(input);
			T obj = new T();
			obj.Deserialize(this);
			m_input.Clear();
			m_context.Clear();
			return new ReadResult<T>(obj, m_errors);
		}
		private ReadResult<T> ReadTaterArray<T>(Tater input) where T : IReadable, new() {
			m_input.Clear();
			m_context.Clear();
			Push(input);
			T[] array = new T[input.Count];
			for (int ix = 0; ix < array.Length; ix++) {
				Push(input[ix]);
				array[ix] = new T();
				array[ix].Deserialize(this);
				Pop();
			}
			m_input.Clear();
			m_context.Clear();
			return new ReadResult<T>(array, m_errors);
		}

		private void Push(Tater value) {
			m_input.Push(value);
			m_context.Add(value.Name);
		}
		private Tater Pop() {
			m_context.RemoveAt(m_context.Count - 1);
			return m_input.Pop();
		}

		private void CheckLogPop() {
			if (m_doPopOnLog) {
				m_doPopOnLog = false;
				Pop();
			}
		}

		private bool DoOptional<T>(string name, ref T value, ReadFunc<T> reader) {
			try {
				value = reader(Current[name]);
				return true;
			} catch {
				CheckLogPop();
				value = default;
				return false;
			}
		}
		private bool DoOptionalCollection<T, U>(string name, ref T collection, ReadFunc<U> reader) where T : ICollection<U>, new() {
			try {
				Tater node = Current[name];
				collection = new T();
				for (int ix = 0; ix < node.Count; ix++) {
					collection.Add(reader(node[ix]));
				}
				return true;
			} catch {
				CheckLogPop();
				collection = default;
				return false;
			}
		}
		private bool DoOptionalReadOnlyList<T>(string name, ref IReadOnlyList<T> rol, ReadFunc<T> reader) {
			try {
				Tater node = Current[name];
				List<T> list = new List<T>(node.Count);
				for (int ix = 0; ix < node.Count; ix++) {
					list.Add(reader(node[ix]));
				}
				rol = list.AsReadOnly();
				return true;
			} catch {
				CheckLogPop();
				rol = default;
				return false;
			}
		}

		private bool DoOptionalArray<T>(string name, ref T[] array, ReadFunc<T> reader) {
			try {
				Tater node = Current[name];
				array = new T[node.Count];
				for (int ix = 0; ix < node.Count; ix++) {
					array[ix] = reader(node[ix]);
				}
				return true;
			} catch {
				CheckLogPop();
				array = default;
				return false;
			}
		}

		private bool DoOptionalDictionary<T, U>(string name, ref T dictionary, ReadFunc<U> reader) where T : IDictionary<string, U>, new() {
			try {
				Tater node = Current[name];
				dictionary = new T();
				foreach (string key in node.Keys) {
					dictionary.Add(key, reader(node[key]));
				}
				return true;
			} catch {
				CheckLogPop();
				dictionary = default;
				return false;
			}
		}

		private void DoRequired<T>(string name, ref T value, ReadFunc<T> reader) {
			if (Current.IsObject && Current.Contains(name)) {
				Push(Current[name]);
				try {
					value = reader(Current);
				} catch (DeserializationException e) {
					LogError(e);
					CheckLogPop();
					value = default;
				} catch (Exception e) {
					LogError(e.Message);
					CheckLogPop();
					value = default;
				}
				Pop();
			} else if (!Current.IsObject) {
				LogError(new DeserializationException(Current, $"{Current.Name} is not an object"));
			} else {
				LogError(new DeserializationException(Current, $"{Current.Name} does not contain field named `{name}'"));
			}
		}
		private void DoRequiredCollection<T, U>(string name, ref T collection, ReadFunc<U> reader) where T : ICollection<U>, new() {
			if (Current.IsObject && Current.Contains(name, TaterType.Array)) {
				Tater node = Current[name];
				Push(node);
				try {
					collection = new T();
					for (int ix = 0; ix < node.Count; ix++) {
						collection.Add(reader(node[ix]));
					}
				} catch (DeserializationException e) {
					LogError(e);
					CheckLogPop();
					collection = default;
				} catch (Exception e) {
					LogError(e.Message);
					CheckLogPop();
					collection = default;
				}
				Pop();
			} else if (!Current.IsObject) {
				LogError(new DeserializationException(Current, $"{Current.Name} is not an object"));
			} else if (Current.Contains(name)) {
				Push(Current[name]);
				LogError(new DeserializationException(Current, $"{Current.Name} is not an array"));
				Pop();
			} else {
				LogError(new DeserializationException(Current, $"{Current.Name} does not contain array named `{name}'"));
			}
		}
		private void DoRequiredArray<T>(string name, ref T[] array, ReadFunc<T> reader) {
			if (Current.IsObject && Current.Contains(name, TaterType.Array)) {
				Tater node = Current[name];
				Push(node);
				try {
					array = new T[node.Count];
					for (int ix = 0; ix < node.Count; ix++) {
						array[ix] = reader(node[ix]);
					}
				} catch (DeserializationException e) {
					LogError(e);
					CheckLogPop();
					array = default;
				} catch (Exception e) {
					LogError(e.Message);
					CheckLogPop();
					array = default;
				}
				Pop();
			} else if (!Current.IsObject) {
				LogError(new DeserializationException(Current, $"{Current.Name} is not an object"));
			} else if (Current.Contains(name)) {
				Push(Current[name]);
				LogError(new DeserializationException(Current, $"{Current.Name} is not an array"));
				Pop();
			} else {
				LogError(new DeserializationException(Current, $"{Current.Name} does not contain array named `{name}'"));
			}
		}
		private void DoRequiredReadOnlyList<T>(string name, ref IReadOnlyList<T> rol, ReadFunc<T> reader) {
			if (Current.IsObject && Current.Contains(name, TaterType.Array)) {
				Tater node = Current[name];
				Push(node);
				try {
					List<T> list = new List<T>(node.Count);
					for (int ix = 0; ix < node.Count; ix++) {
						list.Add(reader(node[ix]));
					}
					rol = list.AsReadOnly();
				} catch (DeserializationException e) {
					LogError(e);
					CheckLogPop();
					rol = default;
				} catch (Exception e) {
					LogError(e.Message);
					CheckLogPop();
					rol = default;
				}
				Pop();
			} else if (!Current.IsObject) {
				LogError(new DeserializationException(Current, $"{Current.Name} is not an object"));
			} else if (Current.Contains(name)) {
				Push(Current[name]);
				LogError(new DeserializationException(Current, $"{Current.Name} is not an array"));
				Pop();
			} else {
				LogError(new DeserializationException(Current, $"{Current.Name} does not contain array named `{name}'"));
			}
		}
		private void DoRequiredDictionary<T, U>(string name, ref T dictionary, ReadFunc<U> reader) where T : IDictionary<string, U>, new() {
			if (Current.IsObject && Current.Contains(name, TaterType.Object)) {
				Tater node = Current[name];
				Push(node);
				try {
					dictionary = new T();
					foreach (string key in node.Keys) {
						dictionary.Add(key, reader(node[key]));
					}
				} catch (DeserializationException e) {
					LogError(e);
					CheckLogPop();
					dictionary = default;
				} catch (Exception e) {
					LogError(e.Message);
					CheckLogPop();
					dictionary = default;
				}
				Pop();
			} else {
				if (!Current.IsObject) {
					LogError(new DeserializationException(Current, $"{Current.Name} is not an object"));
				} else if (Current.Contains(name)) {
					Push(Current[name]);
					LogError(new DeserializationException(Current, $"{Current.Name} is not an object"));
					Pop();
				} else {
					LogError(new DeserializationException(Current, $"{Current.Name} does not contain object named `{name}'"));
				}
			}
		}

		#region Conversions

		private T TaterToObject<T>(Tater tater) where T : IReadable, new() {
			Push(tater);
			T obj = new T();
			obj.Deserialize(this);
			Pop();
			return obj;
		}
		private string TaterToString(Tater tater) {
			return tater.AsString;
		}
		private T TaterToStringProxy<T>(Tater tater) where T : IReadProxy<string>, new() {
			T value = new T();
			value.SetProxyValue(tater.AsString);
			return value;
		}
		private T TaterToEnum<T>(Tater tater) where T : struct, Enum {
			if (Enum.TryParse(tater.AsString, out T result)) {
				return result;
			} else {
				throw new DeserializationException(tater, $"Cannot parse `{tater.AsString}' as valid value of {typeof(T).Name}");
			}
		}
		private int TaterToInt32(Tater tater) {
			return tater.AsInt32;
		}
		private T TaterToInt32Proxy<T>(Tater tater) where T : IReadProxy<int>, new() {
			T value = new T();
			value.SetProxyValue(tater.AsInt32);
			return value;
		}
		private uint TaterToUInt32(Tater tater) {
			return tater.AsUInt32;
		}
		private T TaterToUInt32Proxy<T>(Tater tater) where T : IReadProxy<uint>, new() {
			T value = new T();
			value.SetProxyValue(tater.AsUInt32);
			return value;
		}
		private short TaterToInt16(Tater tater) {
			return tater.AsInt16;
		}
		private T TaterToInt16Proxy<T>(Tater tater) where T : IReadProxy<short>, new() {
			T value = new T();
			value.SetProxyValue(tater.AsInt16);
			return value;
		}
		private ushort TaterToUInt16(Tater tater) {
			return tater.AsUInt16;
		}
		private T TaterToUInt16Proxy<T>(Tater tater) where T : IReadProxy<ushort>, new() {
			T value = new T();
			value.SetProxyValue(tater.AsUInt16);
			return value;
		}
		private long TaterToInt64(Tater tater) {
			return tater.AsInt64;
		}
		private T TaterToInt64Proxy<T>(Tater tater) where T : IReadProxy<long>, new() {
			T value = new T();
			value.SetProxyValue(tater.AsInt64);
			return value;
		}
		private ulong TaterToUInt64(Tater tater) {
			return tater.AsUInt64;
		}
		private T TaterToUInt64Proxy<T>(Tater tater) where T : IReadProxy<ulong>, new() {
			T value = new T();
			value.SetProxyValue(tater.AsUInt64);
			return value;
		}
		private double TaterToDouble(Tater tater) {
			return tater.AsDouble;
		}
		private T TaterToDoubleProxy<T>(Tater tater) where T : IReadProxy<double>, new() {
			T value = new T();
			value.SetProxyValue(tater.AsDouble);
			return value;
		}
		private float TaterToSingle(Tater tater) {
			return tater.AsSingle;
		}
		private T TaterToSingleProxy<T>(Tater tater) where T : IReadProxy<float>, new() {
			T value = new T();
			value.SetProxyValue(tater.AsSingle);
			return value;
		}
		private bool TaterToBool(Tater tater) {
			return tater.AsBool;
		}
		private T TaterToBoolProxy<T>(Tater tater) where T : IReadProxy<bool>, new() {
			T value = new T();
			value.SetProxyValue(tater.AsBool);
			return value;
		}
		private byte TaterToByte(Tater tater) {
			return tater.AsByte;
		}
		private T TaterToByteProxy<T>(Tater tater) where T : IReadProxy<byte>, new() {
			T value = new T();
			value.SetProxyValue(tater.AsByte);
			return value;
		}
		private sbyte TaterToSByte(Tater tater) {
			return tater.AsSByte;
		}
		private T TaterToSByteProxy<T>(Tater tater) where T : IReadProxy<sbyte>, new() {
			T value = new T();
			value.SetProxyValue(tater.AsSByte);
			return value;
		}

		private float PushTaterToSingle(Tater tater) {
			Push(tater);
			try {
				float value = tater.AsSingle;
				Pop();
				return value;
			} catch (Exception e) {
				m_doPopOnLog = true;
				throw e;
			}
		}
		private int PushTaterToInt32(Tater tater) {
			Push(tater);
			try {
				int value = tater.AsInt32;
				Pop();
				return value;
			} catch (Exception e) {
				m_doPopOnLog = true;
				throw e;
			}
		}
		private byte PushTaterToByte(Tater tater) {
			Push(tater);
			try {
				byte value = tater.AsByte;
				Pop();
				return value;
			} catch (Exception e) {
				m_doPopOnLog = true;
				throw e;
			}
		}

		private Vector2 TaterToVector2(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Vector2 value = new Vector2(
				PushTaterToSingle(x), 
				PushTaterToSingle(y)
			);
			return value;
		}

		private Vector3 TaterToVector3(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Tater z = tater["z"];
			Vector3 value = new Vector3(
				PushTaterToSingle(x),
				PushTaterToSingle(y),
				PushTaterToSingle(z)
			);
			return value;
		}
		private Vector4 TaterToVector4(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Tater z = tater["z"];
			Tater w = tater["w"];
			Vector4 value = new Vector4(
				PushTaterToSingle(x),
				PushTaterToSingle(y),
				PushTaterToSingle(z),
				PushTaterToSingle(w)
			);
			return value;
		}
		private Vector2Int TaterToVector2Int(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Vector2Int value = new Vector2Int(
				PushTaterToInt32(x),
				PushTaterToInt32(y)
			);
			return value;
		}
		private Vector3Int TaterToVector3Int(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Tater z = tater["z"];
			Vector3Int value = new Vector3Int(
				PushTaterToInt32(x),
				PushTaterToInt32(y),
				PushTaterToInt32(z)
			);
			return value;
		}
		private Color TaterToColor(Tater tater) {
			Tater r = tater["r"];
			Tater g = tater["g"];
			Tater b = tater["b"];
			Tater a = tater.Contains("a", TaterType.Number) ? tater["a"] : null;
			Color value = new Color(
				PushTaterToSingle(r),
				PushTaterToSingle(g),
				PushTaterToSingle(b),
				(a == null) ? 1f : PushTaterToSingle(a)
			);
			return value;
		}
		private Color32 TaterToColor32(Tater tater) {
			Tater r = tater["r"];
			Tater g = tater["g"];
			Tater b = tater["b"];
			Tater a = tater.Contains("a", TaterType.Number) ? tater["a"] : null;
			Color32 value = new Color32(
				PushTaterToByte(r),
				PushTaterToByte(g),
				PushTaterToByte(b),
				(a == null) ? byte.MaxValue : PushTaterToByte(a)
			);
			return value;
		}

		private Rect TaterToRect(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Tater width = tater["width"];
			Tater height = tater["height"];
			Rect value = new Rect(
				PushTaterToSingle(x),
				PushTaterToSingle(y),
				PushTaterToSingle(width),
				PushTaterToSingle(height)
			);
			return value;
		}
		private RectInt TaterToRectInt(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Tater width = tater["width"];
			Tater height = tater["height"];
			RectInt value = new RectInt(
				PushTaterToInt32(x),
				PushTaterToInt32(y),
				PushTaterToInt32(width),
				PushTaterToInt32(height)
			);
			return value;
		}
		private Quaternion TaterToQuaternion(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Tater z = tater["z"];
			Tater w = tater["w"];
			Quaternion value = new Quaternion(
				PushTaterToSingle(x),
				PushTaterToSingle(y),
				PushTaterToSingle(z),
				PushTaterToSingle(w)
			);
			return value;
		}

		#endregion

		private string BuildPath() {
			m_builder.Clear();
			foreach (string context in m_context) {
				m_builder.Append(context).Append(".");
			}
			--m_builder.Length;
			return m_builder.ToString();
		}

		private void LogError(ParseException exception) {
			m_errors.Add(new ReadError(exception));
		}
		private void LogError(DeserializationException exception) {
			m_errors.Add(new ReadError(exception, BuildPath()));
		}

		#region IReader

		public void LogError(string error) {
			m_errors.Add(new ReadError(error, Current.LineNumber, BuildPath()));
		}

		public Tater GetTater() {
			return Current;
		}

		public bool OptionalObject<T>(string name, ref T value) where T : IReadable, new() {
			return DoOptional(name, ref value, TaterToObject<T>);
		}

		public bool OptionalObjectList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadable, new() {
			return DoOptionalCollection(name, ref value, TaterToObject<U>);
		}
		public bool OptionalObjectReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadable, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToObject<T>);
		}
		public bool OptionalObjectArray<T>(string name, ref T[] value) where T : IReadable, new() {
			return DoOptionalArray(name, ref value, TaterToObject<T>);
		}

		public bool OptionalObjectMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadable, new() {
			return DoOptionalDictionary(name, ref value, TaterToObject<U>);
		}

		public void RequiredObject<T>(string name, ref T value) where T : IReadable, new() {
			DoRequired(name, ref value, TaterToObject<T>);
		}

		public void RequiredObjectList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadable, new() {
			DoRequiredCollection(name, ref value, TaterToObject<U>);
		}

		public void RequiredObjectMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadable, new() {
			DoRequiredDictionary(name, ref value, TaterToObject<U>);
		}
		public void RequiredObjectArray<T>(string name, ref T[] value) where T : IReadable, new() {
			DoRequiredArray(name, ref value, TaterToObject<T>);
		}
		public void RequiredObjectReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadable, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToObject<T>);
		}


		public bool OptionalEnum<T>(string name, ref T value) where T : struct, Enum {
			return DoOptional(name, ref value, TaterToEnum<T>);
		}
		public bool OptionalEnumList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : struct, Enum {
			return DoOptionalCollection(name, ref value, TaterToEnum<U>);
		}
		public bool OptionalEnumReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : struct, Enum {
			return DoOptionalReadOnlyList(name, ref value, TaterToEnum<T>);
		}
		public bool OptionalEnumArray<T>(string name, ref T[] value) where T : struct, Enum {
			return DoOptionalArray(name, ref value, TaterToEnum<T>);
		}
		public bool OptionalEnumMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : struct, Enum {
			return DoOptionalDictionary(name, ref value, TaterToEnum<U>);
		}
		public void RequiredEnum<T>(string name, ref T value) where T : struct, Enum {
			DoRequired(name, ref value, TaterToEnum<T>);
		}
		public void RequiredEnumList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : struct, Enum {
			DoRequiredCollection(name, ref value, TaterToEnum<U>);
		}
		public void RequiredEnumReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : struct, Enum {
			DoRequiredReadOnlyList(name, ref value, TaterToEnum<T>);
		}
		public void RequiredEnumArray<T>(string name, ref T[] value) where T : struct, Enum {
			DoRequiredArray(name, ref value, TaterToEnum<T>);
		}
		public void RequiredEnumMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : struct, Enum {
			DoRequiredDictionary(name, ref value, TaterToEnum<U>);
		}

		#endregion

	}

}