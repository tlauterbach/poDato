using System;
using System.Collections.Generic;

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

		#endregion




	}

}