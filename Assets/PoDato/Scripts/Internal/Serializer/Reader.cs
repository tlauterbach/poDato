using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoDato {

	internal class Reader : IReader {

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
					return true;
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
		private bool DoOptionalDictionary<T, U>(string name, ref T dictionary, ReadFunc<U> reader) where T : IDictionary<string, U>, new() {
			try {
				Tater node = Current[name];
				if (node.IsNull) {
					dictionary = default;
					return true;
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
					collection = default;
					return;
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
		private void DoRequiredDictionary<T, U>(string name, ref T dictionary, ReadFunc<U> reader) where T : IDictionary<string, U>, new() {
			try {
				Tater node = Current[name];
				if (node.IsNull) {
					dictionary = default;
					return;
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

		#region IReadable

		public bool OptionalObject<T>(string name, ref T value) where T : IReadable, new() {
			return DoOptional(name, ref value, TaterToObject<T>);
		}

		public bool OptionalObjectList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadable, new() {
			return DoOptionalCollection(name, ref value, TaterToObject<U>);
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

		#endregion

		#region string

		public bool OptionalString(string name, ref string value) {
			return DoOptional(name, ref value, TaterToString);
		}

		public bool OptionalStringList<T>(string name, ref T value) where T : ICollection<string>, new() {
			return DoOptionalCollection(name, ref value, TaterToString);
		}

		public bool OptionalStringMap<T>(string name, ref T value) where T : IDictionary<string, string>, new() {
			return DoOptionalDictionary(name, ref value, TaterToString);
		}

		public bool OptionalStringProxy<T>(string name, ref T value) where T : IReadProxy<string>, new() {
			return DoOptional(name, ref value, TaterToStringProxy<T>);
		}

		public bool OptionalStringProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<string>, new() {
			return DoOptionalCollection(name, ref value, TaterToStringProxy<U>);
		}

		public bool OptionalStringProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<string>, new() {
			return DoOptionalDictionary(name, ref value, TaterToStringProxy<U>);
		}

		public void RequiredString(string name, ref string value) {
			DoRequired(name, ref value, TaterToString);
		}

		public void RequiredStringList<T>(string name, ref T value) where T : ICollection<string>, new() {
			DoRequiredCollection(name, ref value, TaterToString);
		}

		public void RequiredStringMap<T>(string name, ref T value) where T : IDictionary<string, string>, new() {
			DoRequiredDictionary(name, ref value, TaterToString);
		}

		public void RequiredStringProxy<T>(string name, ref T value) where T : IReadProxy<string>, new() {
			DoRequired(name, ref value, TaterToStringProxy<T>);
		}

		public void RequiredStringProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<string>, new() {
			DoRequiredCollection(name, ref value, TaterToStringProxy<U>);
		}

		public void RequiredStringProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<string>, new() {
			DoRequiredDictionary(name, ref value, TaterToStringProxy<U>);
		}

		#endregion

		#region enum

		public bool OptionalEnum<T>(string name, ref T value) where T : struct, Enum {
			return DoOptional(name, ref value, TaterToEnum<T>);
		}
		public bool OptionalEnumList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : struct, Enum {
			return DoOptionalCollection(name, ref value, TaterToEnum<U>);
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
		public void RequiredEnumMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : struct, Enum {
			DoRequiredDictionary(name, ref value, TaterToEnum<U>);
		}
		#endregion

		#region int

		public bool OptionalInt32(string name, ref int value) {
			return DoOptional(name, ref value, TaterToInt32);
		}

		public bool OptionalInt32List<T>(string name, ref T value) where T : ICollection<int>, new() {
			return DoOptionalCollection(name, ref value, TaterToInt32);
		}

		public bool OptionalInt32Map<T>(string name, ref T value) where T : IDictionary<string, int>, new() {
			return DoOptionalDictionary(name, ref value, TaterToInt32);
		}

		public bool OptionalInt32Proxy<T>(string name, ref T value) where T : IReadProxy<int>, new() {
			return DoOptional(name, ref value, TaterToInt32Proxy<T>);
		}

		public bool OptionalInt32ProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<int>, new() {
			return DoOptionalCollection(name, ref value, TaterToInt32Proxy<U>);
		}

		public bool OptionalInt32ProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<int>, new() {
			return DoOptionalDictionary(name, ref value, TaterToInt32Proxy<U>);
		}

		public void RequiredInt32(string name, ref int value) {
			DoRequired(name, ref value, TaterToInt32);
		}

		public void RequiredInt32List<T>(string name, ref T value) where T : ICollection<int>, new() {
			DoRequiredCollection(name, ref value, TaterToInt32);
		}

		public void RequiredInt32Map<T>(string name, ref T value) where T : IDictionary<string, int>, new() {
			DoRequiredDictionary(name, ref value, TaterToInt32);
		}

		public void RequiredInt32Proxy<T>(string name, ref T value) where T : IReadProxy<int>, new() {
			DoRequired(name, ref value, TaterToInt32Proxy<T>);
		}

		public void RequiredInt32ProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<int>, new() {
			DoRequiredCollection(name, ref value, TaterToInt32Proxy<U>);
		}

		public void RequiredInt32ProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<int>, new() {
			DoRequiredDictionary(name, ref value, TaterToInt32Proxy<U>);
		}
		#endregion

		#region bool

		public bool OptionalBool(string name, ref bool value) {
			return DoOptional(name, ref value, TaterToBool);
		}

		public bool OptionalBoolList<T>(string name, ref T value) where T : ICollection<bool>, new() {
			return DoOptionalCollection(name, ref value, TaterToBool);
		}

		public bool OptionalBoolMap<T>(string name, ref T value) where T : IDictionary<string, bool>, new() {
			return DoOptionalDictionary(name, ref value, TaterToBool);
		}

		public bool OptionalBoolProxy<T>(string name, ref T value) where T : IReadProxy<bool>, new() {
			return DoOptional(name, ref value, TaterToBoolProxy<T>);
		}

		public bool OptionalBoolProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<bool>, new() {
			return DoOptionalCollection(name, ref value, TaterToBoolProxy<U>);
		}

		public bool OptionalBoolProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<bool>, new() {
			return DoOptionalDictionary(name, ref value, TaterToBoolProxy<U>);
		}

		public void RequiredBool(string name, ref bool value) {
			DoRequired(name, ref value, TaterToBool);
		}

		public void RequiredBoolList<T>(string name, ref T value) where T : ICollection<bool>, new() {
			DoRequiredCollection(name, ref value, TaterToBool);
		}

		public void RequiredBoolMap<T>(string name, ref T value) where T : IDictionary<string, bool>, new() {
			DoRequiredDictionary(name, ref value, TaterToBool);
		}

		public void RequiredBoolProxy<T>(string name, ref T value) where T : IReadProxy<bool>, new() {
			DoRequired(name, ref value, TaterToBoolProxy<T>);
		}

		public void RequiredBoolProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<bool>, new() {
			DoRequiredCollection(name, ref value, TaterToBoolProxy<U>);
		}

		public void RequiredBoolProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<bool>, new() {
			DoRequiredDictionary(name, ref value, TaterToBoolProxy<U>);
		}

		#endregion

		#region uint

		public bool OptionalUInt32(string name, ref uint value) {
			return DoOptional(name, ref value, TaterToUInt32);
		}

		public bool OptionalUInt32List<T>(string name, ref T value) where T : ICollection<uint>, new() {
			return DoOptionalCollection(name, ref value, TaterToUInt32);
		}

		public bool OptionalUInt32Map<T>(string name, ref T value) where T : IDictionary<string, uint>, new() {
			return DoOptionalDictionary(name, ref value, TaterToUInt32);
		}

		public bool OptionalUInt32Proxy<T>(string name, ref T value) where T : IReadProxy<uint>, new() {
			return DoOptional(name, ref value, TaterToUInt32Proxy<T>);
		}

		public bool OptionalUInt32ProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<uint>, new() {
			return DoOptionalCollection(name, ref value, TaterToUInt32Proxy<U>);
		}

		public bool OptionalUInt32ProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<uint>, new() {
			return DoOptionalDictionary(name, ref value, TaterToUInt32Proxy<U>);
		}

		public void RequiredUInt32(string name, ref uint value) {
			DoRequired(name, ref value, TaterToUInt32);
		}

		public void RequiredUInt32List<T>(string name, ref T value) where T : ICollection<uint>, new() {
			DoRequiredCollection(name, ref value, TaterToUInt32);
		}

		public void RequiredUInt32Map<T>(string name, ref T value) where T : IDictionary<string, uint>, new() {
			DoRequiredDictionary(name, ref value, TaterToUInt32);
		}

		public void RequiredUInt32Proxy<T>(string name, ref T value) where T : IReadProxy<uint>, new() {
			DoRequired(name, ref value, TaterToUInt32Proxy<T>);
		}

		public void RequiredUInt32ProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<uint>, new() {
			DoRequiredCollection(name, ref value, TaterToUInt32Proxy<U>);
		}

		public void RequiredUInt32ProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<uint>, new() {
			DoRequiredDictionary(name, ref value, TaterToUInt32Proxy<U>);
		}

		#endregion

		#region short

		public bool OptionalInt16(string name, ref short value) {
			return DoOptional(name, ref value, TaterToInt16);
		}

		public bool OptionalInt16List<T>(string name, ref T value) where T : ICollection<short>, new() {
			return DoOptionalCollection(name, ref value, TaterToInt16);
		}

		public bool OptionalInt16Map<T>(string name, ref T value) where T : IDictionary<string, short>, new() {
			return DoOptionalDictionary(name, ref value, TaterToInt16);
		}

		public bool OptionalInt16Proxy<T>(string name, ref T value) where T : IReadProxy<short>, new() {
			return DoOptional(name, ref value, TaterToInt16Proxy<T>);
		}

		public bool OptionalInt16ProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<short>, new() {
			return DoOptionalCollection(name, ref value, TaterToInt16Proxy<U>);
		}

		public bool OptionalInt16ProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<short>, new() {
			return DoOptionalDictionary(name, ref value, TaterToInt16Proxy<U>);
		}

		public void RequiredInt16(string name, ref short value) {
			DoRequired(name, ref value, TaterToInt16);
		}

		public void RequiredInt16List<T>(string name, ref T value) where T : ICollection<short>, new() {
			DoRequiredCollection(name, ref value, TaterToInt16);
		}

		public void RequiredInt16Map<T>(string name, ref T value) where T : IDictionary<string, short>, new() {
			DoRequiredDictionary(name, ref value, TaterToInt16);
		}

		public void RequiredInt16Proxy<T>(string name, ref T value) where T : IReadProxy<short>, new() {
			DoRequired(name, ref value, TaterToInt16Proxy<T>);
		}

		public void RequiredInt16ProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<short>, new() {
			DoRequiredCollection(name, ref value, TaterToInt16Proxy<U>);
		}

		public void RequiredInt16ProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<short>, new() {
			DoRequiredDictionary(name, ref value, TaterToInt16Proxy<U>);
		}

		#endregion

		#region ushort

		public bool OptionalUInt16(string name, ref ushort value) {
			return DoOptional(name, ref value, TaterToUInt16);
		}

		public bool OptionalUInt16List<T>(string name, ref T value) where T : ICollection<ushort>, new() {
			return DoOptionalCollection(name, ref value, TaterToUInt16);
		}

		public bool OptionalUInt16Map<T>(string name, ref T value) where T : IDictionary<string, ushort>, new() {
			return DoOptionalDictionary(name, ref value, TaterToUInt16);
		}

		public bool OptionalUInt16Proxy<T>(string name, ref T value) where T : IReadProxy<ushort>, new() {
			return DoOptional(name, ref value, TaterToUInt16Proxy<T>);
		}

		public bool OptionalUInt16ProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<ushort>, new() {
			return DoOptionalCollection(name, ref value, TaterToUInt16Proxy<U>);
		}

		public bool OptionalUInt16ProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<ushort>, new() {
			return DoOptionalDictionary(name, ref value, TaterToUInt16Proxy<U>);
		}

		public void RequiredUInt16(string name, ref ushort value) {
			DoRequired(name, ref value, TaterToUInt16);
		}

		public void RequiredUInt16List<T>(string name, ref T value) where T : ICollection<ushort>, new() {
			DoRequiredCollection(name, ref value, TaterToUInt16);
		}

		public void RequiredUInt16Map<T>(string name, ref T value) where T : IDictionary<string, ushort>, new() {
			DoRequiredDictionary(name, ref value, TaterToUInt16);
		}

		public void RequiredUInt16Proxy<T>(string name, ref T value) where T : IReadProxy<ushort>, new() {
			DoRequired(name, ref value, TaterToUInt16Proxy<T>);
		}

		public void RequiredUInt16ProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<ushort>, new() {
			DoRequiredCollection(name, ref value, TaterToUInt16Proxy<U>);
		}

		public void RequiredUInt16ProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<ushort>, new() {
			DoRequiredDictionary(name, ref value, TaterToUInt16Proxy<U>);
		}

		#endregion

		#region long

		public bool OptionalInt64(string name, ref long value) {
			return DoOptional(name, ref value, TaterToInt64);
		}

		public bool OptionalInt64List<T>(string name, ref T value) where T : ICollection<long>, new() {
			return DoOptionalCollection(name, ref value, TaterToInt64);
		}

		public bool OptionalInt64Map<T>(string name, ref T value) where T : IDictionary<string, long>, new() {
			return DoOptionalDictionary(name, ref value, TaterToInt64);
		}

		public bool OptionalInt64Proxy<T>(string name, ref T value) where T : IReadProxy<long>, new() {
			return DoOptional(name, ref value, TaterToInt64Proxy<T>);
		}

		public bool OptionalInt64ProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<long>, new() {
			return DoOptionalCollection(name, ref value, TaterToInt64Proxy<U>);
		}

		public bool OptionalInt64ProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<long>, new() {
			return DoOptionalDictionary(name, ref value, TaterToInt64Proxy<U>);
		}

		public void RequiredInt64(string name, ref long value) {
			DoRequired(name, ref value, TaterToInt64);
		}

		public void RequiredInt64List<T>(string name, ref T value) where T : ICollection<long>, new() {
			DoRequiredCollection(name, ref value, TaterToInt64);
		}

		public void RequiredInt64Map<T>(string name, ref T value) where T : IDictionary<string, long>, new() {
			DoRequiredDictionary(name, ref value, TaterToInt64);
		}

		public void RequiredInt64Proxy<T>(string name, ref T value) where T : IReadProxy<long>, new() {
			DoRequired(name, ref value, TaterToInt64Proxy<T>);
		}

		public void RequiredInt64ProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<long>, new() {
			DoRequiredCollection(name, ref value, TaterToInt64Proxy<U>);
		}

		public void RequiredInt64ProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<long>, new() {
			DoRequiredDictionary(name, ref value, TaterToInt64Proxy<U>);
		}

		#endregion

		#region ulong

		public bool OptionalUInt64(string name, ref ulong value) {
			return DoOptional(name, ref value, TaterToUInt64);
		}

		public bool OptionalUInt64List<T>(string name, ref T value) where T : ICollection<ulong>, new() {
			return DoOptionalCollection(name, ref value, TaterToUInt64);
		}

		public bool OptionalUInt64Map<T>(string name, ref T value) where T : IDictionary<string, ulong>, new() {
			return DoOptionalDictionary(name, ref value, TaterToUInt64);
		}

		public bool OptionalUInt64Proxy<T>(string name, ref T value) where T : IReadProxy<ulong>, new() {
			return DoOptional(name, ref value, TaterToUInt64Proxy<T>);
		}

		public bool OptionalUInt64ProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<ulong>, new() {
			return DoOptionalCollection(name, ref value, TaterToUInt64Proxy<U>);
		}

		public bool OptionalUInt64ProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<ulong>, new() {
			return DoOptionalDictionary(name, ref value, TaterToUInt64Proxy<U>);
		}

		public void RequiredUInt64(string name, ref ulong value) {
			DoRequired(name, ref value, TaterToUInt64);
		}

		public void RequiredUInt64List<T>(string name, ref T value) where T : ICollection<ulong>, new() {
			DoRequiredCollection(name, ref value, TaterToUInt64);
		}

		public void RequiredUInt64Map<T>(string name, ref T value) where T : IDictionary<string, ulong>, new() {
			DoRequiredDictionary(name, ref value, TaterToUInt64);
		}

		public void RequiredUInt64Proxy<T>(string name, ref T value) where T : IReadProxy<ulong>, new() {
			DoRequired(name, ref value, TaterToUInt64Proxy<T>);
		}

		public void RequiredUInt64ProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<ulong>, new() {
			DoRequiredCollection(name, ref value, TaterToUInt64Proxy<U>);
		}

		public void RequiredUInt64ProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<ulong>, new() {
			DoRequiredDictionary(name, ref value, TaterToUInt64Proxy<U>);
		}

		#endregion

		#region byte

		public bool OptionalByte(string name, ref byte value) {
			return DoOptional(name, ref value, TaterToByte);
		}

		public bool OptionalByteList<T>(string name, ref T value) where T : ICollection<byte>, new() {
			return DoOptionalCollection(name, ref value, TaterToByte);
		}

		public bool OptionalByteMap<T>(string name, ref T value) where T : IDictionary<string, byte>, new() {
			return DoOptionalDictionary(name, ref value, TaterToByte);
		}

		public bool OptionalByteProxy<T>(string name, ref T value) where T : IReadProxy<byte>, new() {
			return DoOptional(name, ref value, TaterToByteProxy<T>);
		}

		public bool OptionalByteProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<byte>, new() {
			return DoOptionalCollection(name, ref value, TaterToByteProxy<U>);
		}

		public bool OptionalByteProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<byte>, new() {
			return DoOptionalDictionary(name, ref value, TaterToByteProxy<U>);
		}

		public void RequiredByte(string name, ref byte value) {
			DoRequired(name, ref value, TaterToByte);
		}

		public void RequiredByteList<T>(string name, ref T value) where T : ICollection<byte>, new() {
			DoRequiredCollection(name, ref value, TaterToByte);
		}

		public void RequiredByteMap<T>(string name, ref T value) where T : IDictionary<string, byte>, new() {
			DoRequiredDictionary(name, ref value, TaterToByte);
		}

		public void RequiredByteProxy<T>(string name, ref T value) where T : IReadProxy<byte>, new() {
			DoRequired(name, ref value, TaterToByteProxy<T>);
		}

		public void RequiredByteProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<byte>, new() {
			DoRequiredCollection(name, ref value, TaterToByteProxy<U>);
		}

		public void RequiredByteProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<byte>, new() {
			DoRequiredDictionary(name, ref value, TaterToByteProxy<U>);
		}

		#endregion

		#region sbyte

		public bool OptionalSByte(string name, ref sbyte value) {
			return DoOptional(name, ref value, TaterToSByte);
		}

		public bool OptionalSByteList<T>(string name, ref T value) where T : ICollection<sbyte>, new() {
			return DoOptionalCollection(name, ref value, TaterToSByte);
		}

		public bool OptionalSByteMap<T>(string name, ref T value) where T : IDictionary<string, sbyte>, new() {
			return DoOptionalDictionary(name, ref value, TaterToSByte);
		}

		public bool OptionalSByteProxy<T>(string name, ref T value) where T : IReadProxy<sbyte>, new() {
			return DoOptional(name, ref value, TaterToSByteProxy<T>);
		}

		public bool OptionalSByteProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<sbyte>, new() {
			return DoOptionalCollection(name, ref value, TaterToSByteProxy<U>);
		}

		public bool OptionalSByteProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<sbyte>, new() {
			return DoOptionalDictionary(name, ref value, TaterToSByteProxy<U>);
		}

		public void RequiredSByte(string name, ref sbyte value) {
			DoRequired(name, ref value, TaterToSByte);
		}

		public void RequiredSByteList<T>(string name, ref T value) where T : ICollection<sbyte>, new() {
			DoRequiredCollection(name, ref value, TaterToSByte);
		}

		public void RequiredSByteMap<T>(string name, ref T value) where T : IDictionary<string, sbyte>, new() {
			DoRequiredDictionary(name, ref value, TaterToSByte);
		}

		public void RequiredSByteProxy<T>(string name, ref T value) where T : IReadProxy<sbyte>, new() {
			DoRequired(name, ref value, TaterToSByteProxy<T>);
		}

		public void RequiredSByteProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<sbyte>, new() {
			DoRequiredCollection(name, ref value, TaterToSByteProxy<U>);
		}

		public void RequiredSByteProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<sbyte>, new() {
			DoRequiredDictionary(name, ref value, TaterToSByteProxy<U>);
		}

		#endregion

		#region float

		public bool OptionalSingle(string name, ref float value) {
			return DoOptional(name, ref value, TaterToSingle);
		}

		public bool OptionalSingleList<T>(string name, ref T value) where T : ICollection<float>, new() {
			return DoOptionalCollection(name, ref value, TaterToSingle);
		}

		public bool OptionalSingleMap<T>(string name, ref T value) where T : IDictionary<string, float>, new() {
			return DoOptionalDictionary(name, ref value, TaterToSingle);
		}

		public bool OptionalSingleProxy<T>(string name, ref T value) where T : IReadProxy<float>, new() {
			return DoOptional(name, ref value, TaterToSingleProxy<T>);
		}

		public bool OptionalSingleProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<float>, new() {
			return DoOptionalCollection(name, ref value, TaterToSingleProxy<U>);
		}

		public bool OptionalSingleProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<float>, new() {
			return DoOptionalDictionary(name, ref value, TaterToSingleProxy<U>);
		}

		public void RequiredSingle(string name, ref float value) {
			DoRequired(name, ref value, TaterToSingle);
		}

		public void RequiredSingleList<T>(string name, ref T value) where T : ICollection<float>, new() {
			DoRequiredCollection(name, ref value, TaterToSingle);
		}

		public void RequiredSingleMap<T>(string name, ref T value) where T : IDictionary<string, float>, new() {
			DoRequiredDictionary(name, ref value, TaterToSingle);
		}

		public void RequiredSingleProxy<T>(string name, ref T value) where T : IReadProxy<float>, new() {
			DoRequired(name, ref value, TaterToSingleProxy<T>);
		}

		public void RequiredSingleProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<float>, new() {
			DoRequiredCollection(name, ref value, TaterToSingleProxy<U>);
		}

		public void RequiredSingleProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<float>, new() {
			DoRequiredDictionary(name, ref value, TaterToSingleProxy<U>);
		}

		#endregion

		#region double

		public bool OptionalDouble(string name, ref double value) {
			return DoOptional(name, ref value, TaterToDouble);
		}

		public bool OptionalDoubleList<T>(string name, ref T value) where T : ICollection<double>, new() {
			return DoOptionalCollection(name, ref value, TaterToDouble);
		}

		public bool OptionalDoubleMap<T>(string name, ref T value) where T : IDictionary<string, double>, new() {
			return DoOptionalDictionary(name, ref value, TaterToDouble);
		}

		public bool OptionalDoubleProxy<T>(string name, ref T value) where T : IReadProxy<double>, new() {
			return DoOptional(name, ref value, TaterToDoubleProxy<T>);
		}

		public bool OptionalDoubleProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<double>, new() {
			return DoOptionalCollection(name, ref value, TaterToDoubleProxy<U>);
		}

		public bool OptionalDoubleProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<double>, new() {
			return DoOptionalDictionary(name, ref value, TaterToDoubleProxy<U>);
		}

		public void RequiredDouble(string name, ref double value) {
			DoRequired(name, ref value, TaterToDouble);
		}

		public void RequiredDoubleList<T>(string name, ref T value) where T : ICollection<double>, new() {
			DoRequiredCollection(name, ref value, TaterToDouble);
		}

		public void RequiredDoubleMap<T>(string name, ref T value) where T : IDictionary<string, double>, new() {
			DoRequiredDictionary(name, ref value, TaterToDouble);
		}

		public void RequiredDoubleProxy<T>(string name, ref T value) where T : IReadProxy<double>, new() {
			DoRequired(name, ref value, TaterToDoubleProxy<T>);
		}

		public void RequiredDoubleProxyList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<double>, new() {
			DoRequiredCollection(name, ref value, TaterToDoubleProxy<U>);
		}

		public void RequiredDoubleProxyMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<double>, new() {
			DoRequiredDictionary(name, ref value, TaterToDoubleProxy<U>);
		}

		#endregion

		#region Vector2

		public bool OptionalVector2(string name, ref Vector2 value) {
			return DoOptional(name, ref value, TaterToVector2);
		}
		
		public bool OptionalVector2List<T>(string name, ref T value) where T : ICollection<Vector2>, new() {
			return DoOptionalCollection(name, ref value, TaterToVector2);
		}

		public bool OptionalVector2Map<T>(string name, ref T value) where T : IDictionary<string, Vector2>, new() {
			return DoOptionalDictionary(name, ref value, TaterToVector2);
		}

		public void RequiredVector2(string name, ref Vector2 value) {
			DoRequired(name, ref value, TaterToVector2);
		}

		public void RequiredVector2List<T>(string name, ref T value) where T : ICollection<Vector2>, new() {
			DoRequiredCollection(name, ref value, TaterToVector2);
		}

		public void RequiredVector2Map<T>(string name, ref T value) where T : IDictionary<string, Vector2>, new() {
			DoRequiredDictionary(name, ref value, TaterToVector2);
		}

		#endregion

		#region Vector3

		public bool OptionalVector3(string name, ref Vector3 value) {
			return DoOptional(name, ref value, TaterToVector3);
		}

		public bool OptionalVector3List<T>(string name, ref T value) where T : ICollection<Vector3>, new() {
			return DoOptionalCollection(name, ref value, TaterToVector3);
		}

		public bool OptionalVector3Map<T>(string name, ref T value) where T : IDictionary<string, Vector3>, new() {
			return DoOptionalDictionary(name, ref value, TaterToVector3);
		}

		public void RequiredVector3(string name, ref Vector3 value) {
			DoRequired(name, ref value, TaterToVector3);
		}

		public void RequiredVector3List<T>(string name, ref T value) where T : ICollection<Vector3>, new() {
			DoRequiredCollection(name, ref value, TaterToVector3);
		}

		public void RequiredVector3Map<T>(string name, ref T value) where T : IDictionary<string, Vector3>, new() {
			DoRequiredDictionary(name, ref value, TaterToVector3);
		}

		#endregion

		#region Vector4

		public bool OptionalVector4(string name, ref Vector4 value) {
			return DoOptional(name, ref value, TaterToVector4);
		}

		public bool OptionalVector4List<T>(string name, ref T value) where T : ICollection<Vector4>, new() {
			return DoOptionalCollection(name, ref value, TaterToVector4);
		}

		public bool OptionalVector4Map<T>(string name, ref T value) where T : IDictionary<string, Vector4>, new() {
			return DoOptionalDictionary(name, ref value, TaterToVector4);
		}

		public void RequiredVector4(string name, ref Vector4 value) {
			DoRequired(name, ref value, TaterToVector4);
		}

		public void RequiredVector4List<T>(string name, ref T value) where T : ICollection<Vector4>, new() {
			DoRequiredCollection(name, ref value, TaterToVector4);
		}

		public void RequiredVector4Map<T>(string name, ref T value) where T : IDictionary<string, Vector4>, new() {
			DoRequiredDictionary(name, ref value, TaterToVector4);
		}

		#endregion

		#region Vector2Int

		public bool OptionalVector2Int(string name, ref Vector2Int value) {
			return DoOptional(name, ref value, TaterToVector2Int);
		}

		public bool OptionalVector2IntList<T>(string name, ref T value) where T : ICollection<Vector2Int>, new() {
			return DoOptionalCollection(name, ref value, TaterToVector2Int);
		}

		public bool OptionalVector2IntMap<T>(string name, ref T value) where T : IDictionary<string, Vector2Int>, new() {
			return DoOptionalDictionary(name, ref value, TaterToVector2Int);
		}

		public void RequiredVector2Int(string name, ref Vector2Int value) {
			DoRequired(name, ref value, TaterToVector2Int);
		}

		public void RequiredVector2IntList<T>(string name, ref T value) where T : ICollection<Vector2Int>, new() {
			DoRequiredCollection(name, ref value, TaterToVector2Int);
		}

		public void RequiredVector2IntMap<T>(string name, ref T value) where T : IDictionary<string, Vector2Int>, new() {
			DoRequiredDictionary(name, ref value, TaterToVector2Int);
		}

		#endregion

		#region Vector3Int

		public bool OptionalVector3Int(string name, ref Vector3Int value) {
			return DoOptional(name, ref value, TaterToVector3Int);
		}

		public bool OptionalVector3IntList<T>(string name, ref T value) where T : ICollection<Vector3Int>, new() {
			return DoOptionalCollection(name, ref value, TaterToVector3Int);
		}

		public bool OptionalVector3IntMap<T>(string name, ref T value) where T : IDictionary<string, Vector3Int>, new() {
			return DoOptionalDictionary(name, ref value, TaterToVector3Int);
		}

		public void RequiredVector3Int(string name, ref Vector3Int value) {
			DoRequired(name, ref value, TaterToVector3Int);
		}

		public void RequiredVector3IntList<T>(string name, ref T value) where T : ICollection<Vector3Int>, new() {
			DoRequiredCollection(name, ref value, TaterToVector3Int);
		}

		public void RequiredVector3IntMap<T>(string name, ref T value) where T : IDictionary<string, Vector3Int>, new() {
			DoRequiredDictionary(name, ref value, TaterToVector3Int);
		}

		#endregion

		#region Color

		public bool OptionalColor(string name, ref Color value) {
			return DoOptional(name, ref value, TaterToColor);
		}

		public bool OptionalColorList<T>(string name, ref T value) where T : ICollection<Color>, new() {
			return DoOptionalCollection(name, ref value, TaterToColor);
		}

		public bool OptionalColorMap<T>(string name, ref T value) where T : IDictionary<string, Color>, new() {
			return DoOptionalDictionary(name, ref value, TaterToColor);
		}

		public void RequiredColor(string name, ref Color value) {
			DoRequired(name, ref value, TaterToColor);
		}

		public void RequiredColorList<T>(string name, ref T value) where T : ICollection<Color>, new() {
			DoRequiredCollection(name, ref value, TaterToColor);
		}

		public void RequiredColorMap<T>(string name, ref T value) where T : IDictionary<string, Color>, new() {
			DoRequiredDictionary(name, ref value, TaterToColor);
		}

		#endregion

		#region Color32

		public bool OptionalColor32(string name, ref Color32 value) {
			return DoOptional(name, ref value, TaterToColor32);
		}

		public bool OptionalColor32List<T>(string name, ref T value) where T : ICollection<Color32>, new() {
			return DoOptionalCollection(name, ref value, TaterToColor32);
		}

		public bool OptionalColor32Map<T>(string name, ref T value) where T : IDictionary<string, Color32>, new() {
			return DoOptionalDictionary(name, ref value, TaterToColor32);
		}

		public void RequiredColor32(string name, ref Color32 value) {
			DoRequired(name, ref value, TaterToColor32);
		}

		public void RequiredColor32List<T>(string name, ref T value) where T : ICollection<Color32>, new() {
			DoRequiredCollection(name, ref value, TaterToColor32);
		}

		public void RequiredColor32Map<T>(string name, ref T value) where T : IDictionary<string, Color32>, new() {
			DoRequiredDictionary(name, ref value, TaterToColor32);
		}

		#endregion

		#region Rect

		public bool OptionalRect(string name, ref Rect value) {
			return DoOptional(name, ref value, TaterToRect);
		}

		public bool OptionalRectList<T>(string name, ref T value) where T : ICollection<Rect>, new() {
			return DoOptionalCollection(name, ref value, TaterToRect);
		}

		public bool OptionalRectMap<T>(string name, ref T value) where T : IDictionary<string, Rect>, new() {
			return DoOptionalDictionary(name, ref value, TaterToRect);
		}

		public void RequiredRect(string name, ref Rect value) {
			DoRequired(name, ref value, TaterToRect);
		}

		public void RequiredRectList<T>(string name, ref T value) where T : ICollection<Rect>, new() {
			DoRequiredCollection(name, ref value, TaterToRect);
		}

		public void RequiredRectMap<T>(string name, ref T value) where T : IDictionary<string, Rect>, new() {
			DoRequiredDictionary(name, ref value, TaterToRect);
		}

		#endregion

		#region RectInt

		public bool OptionalRectInt(string name, ref RectInt value) {
			return DoOptional(name, ref value, TaterToRectInt);
		}

		public bool OptionalRectIntList<T>(string name, ref T value) where T : ICollection<RectInt>, new() {
			return DoOptionalCollection(name, ref value, TaterToRectInt);
		}

		public bool OptionalRectIntMap<T>(string name, ref T value) where T : IDictionary<string, RectInt>, new() {
			return DoOptionalDictionary(name, ref value, TaterToRectInt);
		}

		public void RequiredRectInt(string name, ref RectInt value) {
			DoRequired(name, ref value, TaterToRectInt);
		}

		public void RequiredRectIntList<T>(string name, ref T value) where T : ICollection<RectInt>, new() {
			DoRequiredCollection(name, ref value, TaterToRectInt);
		}

		public void RequiredRectIntMap<T>(string name, ref T value) where T : IDictionary<string, RectInt>, new() {
			DoRequiredDictionary(name, ref value, TaterToRectInt);
		}

		#endregion

		#region Quaternion

		public bool OptionalQuaternion(string name, ref Quaternion value) {
			return DoOptional(name, ref value, TaterToQuaternion);
		}

		public bool OptionalQuaternionList<T>(string name, ref T value) where T : ICollection<Quaternion>, new() {
			return DoOptionalCollection(name, ref value, TaterToQuaternion);
		}

		public bool OptionalQuaternionMap<T>(string name, ref T value) where T : IDictionary<string, Quaternion>, new() {
			return DoOptionalDictionary(name, ref value, TaterToQuaternion);
		}

		public void RequiredQuaternion(string name, ref Quaternion value) {
			DoRequired(name, ref value, TaterToQuaternion);
		}

		public void RequiredQuaternionList<T>(string name, ref T value) where T : ICollection<Quaternion>, new() {
			DoRequiredCollection(name, ref value, TaterToQuaternion);
		}

		public void RequiredQuaternionMap<T>(string name, ref T value) where T : IDictionary<string, Quaternion>, new() {
			DoRequiredDictionary(name, ref value, TaterToQuaternion);
		}

		#endregion

		#endregion




	}

}