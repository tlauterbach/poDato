using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoDato {

	internal sealed partial class Reader : IReader {

		internal delegate TVal ReadFunc<TVal>(Tater tater);

		private Lexer m_lexer;
		private Parser m_parser;

		private Stack<Tater> m_input;
		private List<string> m_errors;

		private Tater Current { get { return m_input.Peek(); } }

		public Reader(int tabSize = 4) {
			m_lexer = new Lexer(tabSize);
			m_parser = new Parser();
			m_input = new Stack<Tater>();
			m_errors = new List<string>();
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
			} catch (Exception e) {
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
			} catch (Exception e) {
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
			} catch (Exception e) {
				LogError(e);
				return new ReadResult(null, m_errors);
			}
		}
		private ReadResult<T> ReadTaterObject<T>(Tater input) where T : IReadable, new() {
			m_input.Clear();
			m_input.Push(input);
			T obj = new T();
			obj.Deserialize(this);
			m_input.Clear();
			return new ReadResult<T>(obj, m_errors);
		}
		private ReadResult<T> ReadTaterArray<T>(Tater input) where T : IReadable, new() {
			m_input.Clear();
			m_input.Push(input);
			T[] array = new T[input.Count];
			for (int ix = 0; ix < array.Length; ix++) {
				m_input.Push(input[ix]);
				array[ix] = new T();
				array[ix].Deserialize(this);
				m_input.Pop();
			}
			m_input.Clear();
			return new ReadResult<T>(array, m_errors);
		}

		private void Push(Tater value) {
			m_input.Push(value);
		}
		private Tater Pop() {
			return m_input.Pop();
		}

		private void LogError(Exception exception) {
			m_errors.Add(exception.Message);
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
				if (node.IsNull) {
					collection = default;
					return false;
				}
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
				if (node.IsNull) {
					array = default;
					return false;
				}
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
				if (node.IsNull) {
					dictionary = default;
					return false;
				}
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
			} catch (Exception e) {
				LogError(new Exception($"Could not read required field \"{name}\": {e.Message}"));
				value = default;
			}
		}
		private void DoRequiredCollection<T, U>(string name, ref T collection, ReadFunc<U> reader) where T : ICollection<U>, new() {
			try {
				Tater node = Current[name];
				if (node.IsNull) {
					throw new Exception("Value cannot be null");
				}
				collection = new T();
				for (int ix = 0; ix < node.Count; ix++) {
					collection.Add(reader(node[ix]));
				}
			} catch (Exception e) {
				LogError(new Exception($"Could not read required collection \"{name}\": {e.Message}"));
				collection = default;
			}
		}
		private void DoRequiredArray<T>(string name, ref T[] array, ReadFunc<T> reader) {
			try {
				Tater node = Current[name];
				if (node.IsNull) {
					throw new Exception("Value cannot be null");
				}
				array = new T[node.Count];
				for (int ix = 0; ix < node.Count; ix++) {
					array[ix] = reader(node[ix]);
				}
			} catch (Exception e) {
				LogError(new Exception($"Could not read required array \"{name}\": {e.Message}"));
				array = default;
			}
		}
		private void DoRequiredDictionary<T, U>(string name, ref T dictionary, ReadFunc<U> reader) where T : IDictionary<string, U>, new() {
			try {
				Tater node = Current[name];
				if (node.IsNull) {
					throw new Exception("Value cannot be null");
				}
				dictionary = new T();
				foreach (string key in node.Keys) {
					dictionary.Add(key, reader(node[key]));
				}
			} catch (Exception e) {
				LogError(new Exception($"Could not read required dictionary \"{name}\": {e.Message}"));
				dictionary = default;
			}
		}

		#region Conversions

		private T TaterToObject<T>(Tater tater) where T : IReadable, new() {
			if (tater.IsNull) {
				return default;
			} else {
				Push(tater);
				T obj = new T();
				obj.Deserialize(this);
				Pop();
				return obj;
			}
		}
		private string TaterToString(Tater tater) {
			if (tater.IsNull) {
				return string.Empty;
			} else {
				return tater.AsString;
			}
		}
		private T TaterToStringProxy<T>(Tater tater) where T : IReadProxy<string>, new() {
			if (tater.IsNull) {
				return default;
			} else {
				T value = new T();
				value.SetProxyValue(tater.AsString);
				return value;
			}
		}
		private T TaterToEnum<T>(Tater tater) where T : struct, Enum {
			if (tater.IsNull) {
				return default;
			} else {
				if (Enum.TryParse(tater.AsString, out T result)) {
					return result;
				} else {
					throw new Exception($"Cannot parse `{tater.AsString}' as valid value of {typeof(T).Name}");
				}
			}
		}
		private int TaterToInt32(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				return tater.AsInt32;
			}
		}
		private T TaterToInt32Proxy<T>(Tater tater) where T : IReadProxy<int>, new() {
			if (tater.IsNull) {
				return default;
			} else {
				T value = new T();
				value.SetProxyValue(tater.AsInt32);
				return value;
			}
		}
		private uint TaterToUInt32(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				return tater.AsUInt32;
			}
		}
		private T TaterToUInt32Proxy<T>(Tater tater) where T : IReadProxy<uint>, new() {
			if (tater.IsNull) {
				return default;
			} else {
				T value = new T();
				value.SetProxyValue(tater.AsUInt32);
				return value;
			}
		}
		private short TaterToInt16(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				return tater.AsInt16;
			}
		}
		private T TaterToInt16Proxy<T>(Tater tater) where T : IReadProxy<short>, new() {
			if (tater.IsNull) {
				return default;
			} else {
				T value = new T();
				value.SetProxyValue(tater.AsInt16);
				return value;
			}
		}
		private ushort TaterToUInt16(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				return tater.AsUInt16;
			}
		}
		private T TaterToUInt16Proxy<T>(Tater tater) where T : IReadProxy<ushort>, new() {
			if (tater.IsNull) {
				return default;
			} else {
				T value = new T();
				value.SetProxyValue(tater.AsUInt16);
				return value;
			}
		}
		private long TaterToInt64(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				return tater.AsInt64;
			}
		}
		private T TaterToInt64Proxy<T>(Tater tater) where T : IReadProxy<long>, new() {
			if (tater.IsNull) {
				return default;
			} else {
				T value = new T();
				value.SetProxyValue(tater.AsInt64);
				return value;
			}
		}
		private ulong TaterToUInt64(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				return tater.AsUInt64;
			}
		}
		private T TaterToUInt64Proxy<T>(Tater tater) where T : IReadProxy<ulong>, new() {
			if (tater.IsNull) {
				return default;
			} else {
				T value = new T();
				value.SetProxyValue(tater.AsUInt64);
				return value;
			}
		}
		private double TaterToDouble(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				return tater.AsDouble;
			}
		}
		private T TaterToDoubleProxy<T>(Tater tater) where T : IReadProxy<double>, new() {
			if (tater.IsNull) {
				return default;
			} else {
				T value = new T();
				value.SetProxyValue(tater.AsDouble);
				return value;
			}
		}
		private float TaterToSingle(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				return tater.AsSingle;
			}
		}
		private T TaterToSingleProxy<T>(Tater tater) where T : IReadProxy<float>, new() {
			if (tater.IsNull) {
				return default;
			} else {
				T value = new T();
				value.SetProxyValue(tater.AsSingle);
				return value;
			}
		}
		private bool TaterToBool(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				return tater.AsBool;
			}
		}
		private T TaterToBoolProxy<T>(Tater tater) where T : IReadProxy<bool>, new() {
			if (tater.IsNull) {
				return default;
			} else {
				T value = new T();
				value.SetProxyValue(tater.AsBool);
				return value;
			}
		}
		private byte TaterToByte(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				return tater.AsByte;
			}
		}
		private T TaterToByteProxy<T>(Tater tater) where T : IReadProxy<byte>, new() {
			if (tater.IsNull) {
				return default;
			} else {
				T value = new T();
				value.SetProxyValue(tater.AsByte);
				return value;
			}
		}
		private sbyte TaterToSByte(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				return tater.AsSByte;
			}
		}
		private T TaterToSByteProxy<T>(Tater tater) where T : IReadProxy<sbyte>, new() {
			if (tater.IsNull) {
				return default;
			} else {
				T value = new T();
				value.SetProxyValue(tater.AsSByte);
				return value;
			}
		}
		private Vector2 TaterToVector2(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				Vector2 value = new Vector2(
					tater["x"].AsSingle,
					tater["y"].AsSingle
				);
				return value;
			}
		}
		private Vector3 TaterToVector3(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				Vector3 value = new Vector3(
					tater["x"].AsSingle,
					tater["y"].AsSingle,
					tater["z"].AsSingle
				);
				return value;
			}
		}
		private Vector4 TaterToVector4(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				Vector4 value = new Vector4(
					tater["x"].AsSingle,
					tater["y"].AsSingle,
					tater["z"].AsSingle,
					tater["w"].AsSingle
				);
				return value;
			}
		}
		private Vector2Int TaterToVector2Int(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				Vector2Int value = new Vector2Int(
					tater["x"].AsInt32,
					tater["y"].AsInt32
				);
				return value;
			}
		}
		private Vector3Int TaterToVector3Int(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				Vector3Int value = new Vector3Int(
					tater["x"].AsInt32,
					tater["y"].AsInt32,
					tater["z"].AsInt32
				);
				return value;
			}
		}
		private Color TaterToColor(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				Color value = new Color(
					tater["r"].AsSingle,
					tater["g"].AsSingle,
					tater["b"].AsSingle,
					tater.Contains("a",TaterType.Number) ? tater["a"].AsInt32 : 1f
				);
				return value;
			}
		}
		private Color32 TaterToColor32(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				Color32 value = new Color32(
					tater["r"].AsByte,
					tater["g"].AsByte,
					tater["b"].AsByte,
					tater.Contains("a", TaterType.Number) ? tater["a"].AsByte : byte.MaxValue
				);
				return value;
			}
		}

		private Rect TaterToRect(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				Rect value = new Rect(
					tater["x"].AsSingle,
					tater["y"].AsSingle,
					tater["width"].AsSingle,
					tater["height"].AsSingle
				);
				return value;
			}
		}
		private RectInt TaterToRectInt(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				RectInt value = new RectInt(
					tater["x"].AsInt32,
					tater["y"].AsInt32,
					tater["width"].AsInt32,
					tater["height"].AsInt32
				);
				return value;
			}
		}
		private Quaternion TaterToQuaternion(Tater tater) {
			if (tater.IsNull) {
				return default;
			} else {
				Quaternion value = new Quaternion(
					tater["x"].AsSingle,
					tater["y"].AsSingle,
					tater["z"].AsSingle,
					tater["w"].AsSingle
				);
				return value;
			}
		}

		#endregion

		#region IReader

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