using System;
using System.Collections.Generic;

namespace PoDato {

	public class Tater {

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
					throw InvalidCast(m_type, TaterType.String);
				}
			}
		}
		public bool AsBool {
			get {
				if (IsBoolean) {
					return m_boolean;
				} else {
					throw InvalidCast(m_type, TaterType.Boolean);
				}
			}
		}
		public double AsDouble {
			get {
				if (IsNumber) {
					return m_number;
				} else {
					throw InvalidCast(m_type, TaterType.Number);
				}
			}
		}
		public float AsSingle {
			get {
				if (IsNumber) {
					return Convert.ToSingle(m_number);
				} else {
					throw InvalidCast(m_type, TaterType.Number);
				}
			}
		}
		public int AsInt32 {
			get {
				if (IsNumber) {
					return Convert.ToInt32(m_number);
				} else {
					throw InvalidCast(m_type, TaterType.Number);
				}
			}
		}
		public short AsInt16 {
			get {
				if (IsNumber) {
					return Convert.ToInt16(m_number);
				} else {
					throw InvalidCast(m_type, TaterType.Number);
				}
			}
		}
		public long AsInt64 {
			get {
				if (IsNumber) {
					return Convert.ToInt64(m_number);
				} else {
					throw InvalidCast(m_type, TaterType.Number);
				}
			}
		}
		public uint AsUInt32 {
			get {
				if (IsNumber) {
					return Convert.ToUInt32(m_number);
				} else {
					throw InvalidCast(m_type, TaterType.Number);
				}
			}
		}
		public ushort AsUInt16 {
			get {
				if (IsNumber) {
					return Convert.ToUInt16(m_number);
				} else {
					throw InvalidCast(m_type, TaterType.Number);
				}
			}
		}
		public ulong AsUInt64 {
			get {
				if (IsNumber) {
					return Convert.ToUInt64(m_number);
				} else {
					throw InvalidCast(m_type, TaterType.Number);
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
					throw InvalidCast(m_type, TaterType.Array);
				}	
			}
		}
		public Tater this[string key] {
			get {
				if (IsObject) {
					if (m_object.ContainsKey(key)) {
						return m_object[key];
					} else {
						return CreateNull();
					}
				} else {
					throw InvalidCast(m_type, TaterType.Object);
				}
			}
		}

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

		public static Tater CreateArray() {
			Tater obj = new Tater();
			obj.m_array = new List<Tater>();
			obj.m_type = TaterType.Array;
			return obj;
		}
		public static Tater CreateObject() {
			Tater obj = new Tater();
			obj.m_object = new Dictionary<string, Tater>();
			obj.m_type = TaterType.Object;
			return obj;
		}
		public static Tater CreateNull() {
			Tater obj = new Tater();
			obj.m_type = TaterType.Null;
			return obj;
		}
		public static Tater CreateString(string value) {
			Tater obj = new Tater();
			obj.m_type = TaterType.String;
			obj.m_string = value;
			return obj;
		}
		public static Tater CreateNumber(double value) {
			Tater obj = new Tater();
			obj.m_type = TaterType.Number;
			obj.m_number = value;
			return obj;
		}
		public static Tater CreateBoolean(bool value) {
			Tater obj = new Tater();
			obj.m_type = TaterType.Boolean;
			obj.m_boolean = value;
			return obj;
		}

		#endregion

		private static InvalidCastException InvalidCast(TaterType from, TaterType to) {
			return new InvalidCastException(
				$"Cannot cast from type `{from}' to type `{to}'"
			);
		}

		public void Add(string key, Tater value) {
			if (IsObject) {
				m_object.Add(key, value);
			} else {
				throw InvalidCast(m_type, TaterType.Object);
			}
		}
		public void Add(Tater value) {
			if (IsArray) {
				m_array.Add(value);
			} else {
				throw InvalidCast(m_type, TaterType.Array);
			}
		}
		public bool Contains(string key) {
			if (IsObject) {
				return m_object.ContainsKey(key);
			} else {
				throw InvalidCast(m_type, TaterType.Object);
			}
		}
		public bool Contains(string key, TaterType type) {
			if (IsObject) {
				return m_object.ContainsKey(key) && m_object[key].IsType(type);
			} else {
				throw InvalidCast(m_type, TaterType.Object);
			}
		}
		public bool IsType(TaterType type) {
			return m_type == type;
		}

	}

}