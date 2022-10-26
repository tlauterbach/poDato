using System;
using System.Collections;
using System.Collections.Generic;

namespace PoDato {

	public class Tater {

		public string Name { get { return m_name; } }
		public TaterType Type { get { return m_type; } }
		public bool IsNull { get { return IsType(TaterType.Null); } }
		public bool IsObject { get { return IsType(TaterType.Object); } }
		public bool IsArray { get { return IsType(TaterType.Array); } }
		public bool IsNumber { get { return IsType(TaterType.Number); } }
		public bool IsString { get { return IsType(TaterType.String); } }
		public bool IsBoolean { get { return IsType(TaterType.Boolean); } }

		#region Conversions

		public string AsString {
			get { 
				if (IsString) {
					return m_string;
				} else {
					throw InvalidCast(this, TaterType.String);
				}
			}
		}
		public bool AsBool {
			get {
				if (IsBoolean) {
					return m_boolean;
				} else {
					throw InvalidCast(this, TaterType.Boolean);
				}
			}
		}
		public double AsDouble {
			get {
				if (IsNumber) {
					return m_number;
				} else {
					throw InvalidCast(this, TaterType.Number);
				}
			}
		}
		public float AsSingle {
			get {
				if (IsNumber) {
					return Convert.ToSingle(m_number);
				} else {
					throw InvalidCast(this, TaterType.Number);
				}
			}
		}
		public int AsInt32 {
			get {
				if (IsNumber) {
					return Convert.ToInt32(m_number);
				} else {
					throw InvalidCast(this, TaterType.Number);
				}
			}
		}
		public short AsInt16 {
			get {
				if (IsNumber) {
					return Convert.ToInt16(m_number);
				} else {
					throw InvalidCast(this, TaterType.Number);
				}
			}
		}
		public long AsInt64 {
			get {
				if (IsNumber) {
					return Convert.ToInt64(m_number);
				} else {
					throw InvalidCast(this, TaterType.Number);
				}
			}
		}
		public uint AsUInt32 {
			get {
				if (IsNumber) {
					return Convert.ToUInt32(m_number);
				} else {
					throw InvalidCast(this, TaterType.Number);
				}
			}
		}
		public ushort AsUInt16 {
			get {
				if (IsNumber) {
					return Convert.ToUInt16(m_number);
				} else {
					throw InvalidCast(this, TaterType.Number);
				}
			}
		}
		public ulong AsUInt64 {
			get {
				if (IsNumber) {
					return Convert.ToUInt64(m_number);
				} else {
					throw InvalidCast(this, TaterType.Number);
				}
			}
		}
		public byte AsByte {
			get {
				if (IsNumber) {
					return Convert.ToByte(m_number);
				} else {
					throw InvalidCast(this, TaterType.Number);
				}
			}
		}
		public sbyte AsSByte {
			get {
				if (IsNumber) {
					return Convert.ToSByte(m_number);
				} else {
					throw InvalidCast(this, TaterType.Number);
				}
			}
		}

		#endregion

		public Tater this[int index] {
			get {
				if (IsArray) {
					if (index < 0 || index >= m_array.Count) {
						throw new IndexOutOfRangeException();
					}
					return m_array[index];
				} else {
					throw InvalidCast(this, TaterType.Array);
				}	
			}
			set {
				if (IsArray) {
					if (index < 0 || index >= m_array.Count) {
						throw new IndexOutOfRangeException();
					}
					m_array[index] = value;
				} else {
					throw InvalidCast(this, TaterType.Array);
				}
			}
		}
		public Tater this[string key] {
			get {
				if (IsObject) {
					if (m_object.ContainsKey(key)) {
						return m_object[key];
					} else {
						throw new KeyNotFoundException($"`{m_name}' does not contain key `{key}'");
					}
				} else {
					throw InvalidCast(this, TaterType.Object);
				}
			}
			set {
				if (IsObject) {
					if (m_object.ContainsKey(key)) {
						m_object[key] = value;
					} else {
						m_object.Add(key, value);
					}
				} else {
					throw InvalidCast(this, TaterType.Object);
				}
			}
		}
		public int Count {
			get {
				if (IsObject) {
					return m_object.Count;
				} else if (IsArray) {
					return m_array.Count;
				} else {
					throw InvalidCast(this, TaterType.Array);
				}
			}
		}
		public IEnumerable<string> Keys {
			get {
				if (IsObject) {
					foreach (string key in m_object.Keys) {
						yield return key;
					}
				} else {
					throw InvalidCast(this, TaterType.Object);
				}
			}
		}
		public IEnumerable<Tater> Values {
			get {
				if (IsObject) {
					foreach (string key in m_object.Keys) {
						yield return m_object[key];
					}
				} else if (IsArray) {
					foreach (Tater tater in m_array) {
						yield return tater;
					}
				} else {
					throw InvalidCast(this, TaterType.Object);
				}
			}
		}
		public IEnumerable<KeyValuePair<string, Tater>> KeyValuePairs {
			get {
				if (IsObject) {
					foreach (KeyValuePair<string, Tater> kvp in m_object) {
						yield return kvp;
					}
				} else {
					throw InvalidCast(this, TaterType.Object);
				}
			}
		}

		private string m_name;
		private TaterType m_type;
		private Dictionary<string, Tater> m_object;
		private List<Tater> m_array;
		private string m_string;
		private double m_number;
		private bool m_boolean;

		private Tater() {
			// obfuscated constructor
		}

		#region Constructor Functions

		public static Tater CreateArray(string name) {
			Tater obj = new Tater();
			obj.m_name = name;
			obj.m_array = new List<Tater>();
			obj.m_type = TaterType.Array;
			return obj;
		}
		public static Tater CreateObject(string name) {
			Tater obj = new Tater();
			obj.m_name = name;
			obj.m_object = new Dictionary<string, Tater>();
			obj.m_type = TaterType.Object;
			return obj;
		}
		public static Tater CreateNull(string name) {
			Tater obj = new Tater();
			obj.m_name = name;
			obj.m_type = TaterType.Null;
			return obj;
		}
		public static Tater CreateString(string name, string value) {
			Tater obj = new Tater();
			obj.m_name = name;
			obj.m_type = TaterType.String;
			obj.m_string = value;
			return obj;
		}
		public static Tater CreateNumber(string name, double value) {
			Tater obj = new Tater();
			obj.m_name = name;
			obj.m_type = TaterType.Number;
			obj.m_number = value;
			return obj;
		}
		public static Tater CreateBoolean(string name, bool value) {
			Tater obj = new Tater();
			obj.m_name = name;
			obj.m_type = TaterType.Boolean;
			obj.m_boolean = value;
			return obj;
		}

		#endregion

		private static InvalidCastException InvalidCast(Tater from, TaterType to) {
			return new InvalidCastException(
				$"Cannot cast field `{from.Name}' from type `{from.Type}' to `{to}'"
			);
		}

		public void Add(string key, Tater value) {
			if (IsObject) {
				m_object.Add(key, value);
			} else {
				throw InvalidCast(this, TaterType.Object);
			}
		}
		public void Add(Tater value) {
			if (IsArray) {
				m_array.Add(value);
			} else {
				throw InvalidCast(this, TaterType.Array);
			}
		}
		public bool Contains(string key) {
			if (IsObject) {
				return m_object.ContainsKey(key);
			} else {
				throw InvalidCast(this, TaterType.Object);
			}
		}
		public bool Contains(string key, TaterType type) {
			if (IsObject) {
				return m_object.ContainsKey(key) && m_object[key].IsType(type);
			} else {
				throw InvalidCast(this, TaterType.Object);
			}
		}
		public bool IsType(TaterType type) {
			return m_type == type;
		}

	}

}