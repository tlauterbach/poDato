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

		private bool DoOptional<T>(string name, ref T value, ReadFunc<T> reader) {
			try {
				value = reader(Current[name]);
				return true;
			} catch {
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
				collection = default;
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
				dictionary = default;
				return false;
			}
		}

		private void DoRequired<T>(string name, ref T value, ReadFunc<T> reader) {
			try {
				value = reader(Current[name]);
			} catch (DeserializationException e) {
				LogError(e);
				value = default;
			} catch (Exception e) {
				LogError(e.Message);
				value = default;
			}
		}
		private void DoRequiredCollection<T, U>(string name, ref T collection, ReadFunc<U> reader) where T : ICollection<U>, new() {
			try {
				Tater node = Current[name];
				collection = new T();
				for (int ix = 0; ix < node.Count; ix++) {
					collection.Add(reader(node[ix]));
				}
			} catch (DeserializationException e) {
				LogError(e);
				collection = default;
			} catch (Exception e) {
				LogError(e.Message);
				collection = default;
			}
		}
		private void DoRequiredArray<T>(string name, ref T[] array, ReadFunc<T> reader) {
			try {
				Tater node = Current[name];
				array = new T[node.Count];
				for (int ix = 0; ix < node.Count; ix++) {
					array[ix] = reader(node[ix]);
				}
			} catch (DeserializationException e) {
				LogError(e);
				array = default;
			} catch (Exception e) {
				LogError(e.Message);
				array = default;
			}
		}
		private void DoRequiredDictionary<T, U>(string name, ref T dictionary, ReadFunc<U> reader) where T : IDictionary<string, U>, new() {
			try {
				Tater node = Current[name];
				dictionary = new T();
				foreach (string key in node.Keys) {
					dictionary.Add(key, reader(node[key]));
				}
			} catch (DeserializationException e) {
				LogError(e);
				dictionary = default;
			} catch (Exception e) {
				LogError(e.Message);
				dictionary = default;
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
				throw new DeserializationException(tater,$"Cannot parse `{tater.AsString}' as valid value of {typeof(T).Name}");
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
		private Vector2 TaterToVector2(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Vector2 value;
			Push(tater);
			try {
				value = new Vector2(x.AsSingle, y.AsSingle);
			} catch (DeserializationException e) {
				LogError(e);
				value = default;
			}
			Pop();
			return value;
			
		}
		private Vector3 TaterToVector3(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Tater z = tater["z"];
			Push(tater);
			Vector3 value;
			try {
				value = new Vector3(x.AsSingle, y.AsSingle, z.AsSingle);
			} catch (DeserializationException e) {
				LogError(e);
				value = default;
			}
			Pop();
			return value;
		}
		private Vector4 TaterToVector4(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Tater z = tater["z"];
			Tater w = tater["w"];
			Push(tater);
			Vector4 value;
			try {
				value = new Vector4(x.AsSingle, y.AsSingle, z.AsSingle, w.AsSingle);
			} catch (DeserializationException e) {
				LogError(e);
				value = default;
			}
			Pop();
			return value;
		}
		private Vector2Int TaterToVector2Int(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Push(tater);
			Vector2Int value;
			try {
				value = new Vector2Int(x.AsInt32, y.AsInt32);
			} catch (DeserializationException e) {
				LogError(e);
				value = default;
			}
			Pop();
			return value;
		}
		private Vector3Int TaterToVector3Int(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Tater z = tater["z"];
			Push(tater);
			Vector3Int value;
			try {
				value = new Vector3Int(x.AsInt32, y.AsInt32, z.AsInt32);
			} catch (DeserializationException e) {
				LogError(e);
				value = default;
			}
			Pop();
			return value;
		}
		private Color TaterToColor(Tater tater) {
			Tater r = tater["r"];
			Tater g = tater["g"];
			Tater b = tater["b"];
			Tater a = tater.Contains("a", TaterType.Number) ? tater["a"] : null;
			Push(tater);
			Color value;
			try {
				value = new Color(r.AsSingle, g.AsSingle, b.AsSingle, a?.AsSingle ?? 1f);
			} catch (DeserializationException e) {
				LogError(e);
				value = default;
			}
			Pop();
			return value;
		}
		private Color32 TaterToColor32(Tater tater) {
			Tater r = tater["r"];
			Tater g = tater["g"];
			Tater b = tater["b"];
			Tater a = tater.Contains("a", TaterType.Number) ? tater["a"] : null;
			Push(tater);
			Color32 value;
			try {
				value = new Color32(r.AsByte, g.AsByte, b.AsByte, a?.AsByte ?? byte.MaxValue);
			} catch (DeserializationException e) {
				LogError(e);
				value = default;
			}
			Pop();
			return value;
		}

		private Rect TaterToRect(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Tater width = tater["width"];
			Tater height = tater["height"];
			Push(tater);
			Rect value;
			try {
				value = new Rect(x.AsSingle, y.AsSingle, width.AsSingle, height.AsSingle);
			} catch (DeserializationException e) {
				LogError(e);
				value = default;
			}
			Pop();
			return value;
		}
		private RectInt TaterToRectInt(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Tater width = tater["width"];
			Tater height = tater["height"];
			Push(tater);
			RectInt value;
			try {
				value = new RectInt(x.AsInt32, y.AsInt32, width.AsInt32, height.AsInt32);
			} catch (DeserializationException e) {
				LogError(e);
				value = default;
			}
			Pop();
			return value;
		}
		private Quaternion TaterToQuaternion(Tater tater) {
			Tater x = tater["x"];
			Tater y = tater["y"];
			Tater z = tater["z"];
			Tater w = tater["w"];
			Push(tater);
			Quaternion value;
			try {
				value = new Quaternion(x.AsSingle, y.AsSingle, z.AsSingle, w.AsSingle);
			} catch (DeserializationException e) {
				LogError(e);
				value = default;
			}
			Pop();
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
		private string BuildPath(Tater current) {
			m_builder.Clear();
			foreach (string context in m_context) {
				m_builder.Append(context).Append(".");
			}
			m_builder.Append(current.Name);
			return m_builder.ToString();
		}

		private void LogError(ParseException exception) {
			m_errors.Add(new ReadError(exception));
		}
		private void LogError(DeserializationException exception) {
			m_errors.Add(new ReadError(exception, BuildPath(exception.Tater)));
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


		public bool OptionalEnum<T>(string name, ref T value) where T : struct, Enum {
			return DoOptional(name, ref value, TaterToEnum<T>);
		}
		public bool OptionalEnumList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : struct, Enum {
			return DoOptionalCollection(name, ref value, TaterToEnum<U>);
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
		public void RequiredEnumArray<T>(string name, ref T[] value) where T : struct, Enum {
			DoRequiredArray(name, ref value, TaterToEnum<T>);
		}
		public void RequiredEnumMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : struct, Enum {
			DoRequiredDictionary(name, ref value, TaterToEnum<U>);
		}

		#endregion

	}

}