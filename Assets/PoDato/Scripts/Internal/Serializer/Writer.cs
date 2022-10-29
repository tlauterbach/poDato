using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoDato {

	internal sealed partial class Writer : IWriter {

		internal delegate Tater WriteFunc<T>(string name, T value);

		private Stack<Tater> m_output;
		private Stringifier m_stringifier;

		private Tater Current { get { return m_output.Peek(); } }

		public Writer(byte tabSize) {
			m_output = new Stack<Tater>();
			m_stringifier = new Stringifier(tabSize);
		}

		public string Write(IWritable item) {
			m_output.Clear();
			m_output.Push(Tater.CreateObject("root_object"));
			item.Serialize(this);
			string value = m_stringifier.Stringify(m_output.Peek());
			m_output.Clear();
			return value;
		}
		public string Write(IEnumerable<IWritable> items) {
			m_output.Clear();
			m_output.Push(Tater.CreateArray("root_array"));
			int ix = 0;
			foreach (IWritable item in items) {
				Tater obj = Tater.CreateObject($"root_array_{ix}");
				Current.Add(obj);
				m_output.Push(obj);
				item.Serialize(this);
				m_output.Pop();
				ix++;
			}
			string value = m_stringifier.Stringify(m_output.Peek());
			m_output.Clear();
			return value;
		}
		public string Write(Tater tater) {
			return m_stringifier.Stringify(tater);
		}

		private void DoValue<T>(string name, T value, WriteFunc<T> writer) {
			Current.Add(name, writer(name, value));
		}
		private void DoCollection<T>(string name, IEnumerable<T> collection, WriteFunc<T> writer) {
			Tater array = Tater.CreateArray(name);
			int index = 0;
			m_output.Push(array);
			foreach (T item in collection) {
				array.Add(writer($"{name}_{index}", item));
				index++;
			}
			m_output.Pop();
		}
		private void DoDictionary<T>(string name, IDictionary<string,T> dictionary, WriteFunc<T> writer) {
			Tater obj = Tater.CreateObject(name);
			m_output.Push(obj);
			foreach (KeyValuePair<string,T> kvp in dictionary) {
				obj.Add(kvp.Key, writer(kvp.Key, kvp.Value));
			}
			m_output.Pop();
		}

		#region Conversions

		private Tater ObjectToTater(string name, IWritable value) {
			if (value == null) {
				throw new NullReferenceException($"value `{name}' is not set to instance of an object");
			}
			Tater obj = Tater.CreateObject(name);
			m_output.Push(obj);
			value.Serialize(this);
			m_output.Pop();
			return obj;
		}
		private Tater EnumToTater(string name, Enum value) {
			return Tater.CreateString(name, value.ToString());
		}
		private Tater StringToTater(string name, string value) {
			return Tater.CreateString(name, value);
		}
		private Tater StringProxyToTater<T>(string name, T value) where T : IWriteProxy<string> {
			string str = value.GetProxyValue();
			return Tater.CreateString(name, str);
		}
		private Tater Int32ToTater(string name, int value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater Int32ProxyToTater<T>(string name, T value) where T : IWriteProxy<int> {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater UInt32ToTater(string name, uint value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater UInt32ProxyToTater<T>(string name, T value) where T : IWriteProxy<uint> {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater Int16ToTater(string name, short value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater Int16ProxyToTater<T>(string name, T value) where T : IWriteProxy<short> {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater UInt16ToTater(string name, ushort value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater UInt16ProxyToTater<T>(string name, T value) where T : IWriteProxy<ushort> {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater Int64ToTater(string name, long value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater Int64ProxyToTater<T>(string name, T value) where T : IWriteProxy<long> {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater UInt64ToTater(string name, ulong value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater UInt64ProxyToTater<T>(string name, T value) where T : IWriteProxy<ulong> {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater DoubleToTater(string name, double value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater DoubleProxyToTater<T>(string name, T value) where T : IWriteProxy<double> {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater SingleToTater(string name, float value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater SingleProxyToTater<T>(string name, T value) where T : IWriteProxy<float> {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater ByteToTater(string name, byte value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater ByteProxyToTater<T>(string name, T value) where T : IWriteProxy<byte> {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater SByteToTater(string name, sbyte value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater SByteProxyToTater<T>(string name, T value) where T : IWriteProxy<sbyte> {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater BoolToTater(string name, bool value) {
			return Tater.CreateBoolean(name, value);
		}
		private Tater BoolProxyToTater<T>(string name, T value) where T : IWriteProxy<bool> {
			return Tater.CreateBoolean(name, value.GetProxyValue());
		}
		private Tater Vector2ToTater(string name, Vector2 value) {
			Tater obj = Tater.CreateObject(name);
			obj.Add("x", Tater.CreateNumber("x", value.x));
			obj.Add("y", Tater.CreateNumber("y", value.y));
			return obj;
		}
		private Tater Vector3ToTater(string name, Vector3 value) {
			Tater obj = Tater.CreateObject(name);
			obj.Add("x", Tater.CreateNumber("x", value.x));
			obj.Add("y", Tater.CreateNumber("y", value.y));
			obj.Add("z", Tater.CreateNumber("z", value.z));
			return obj;
		}
		private Tater Vector4ToTater(string name, Vector4 value) {
			Tater obj = Tater.CreateObject(name);
			obj.Add("x", Tater.CreateNumber("x", value.x));
			obj.Add("y", Tater.CreateNumber("y", value.y));
			obj.Add("z", Tater.CreateNumber("z", value.z));
			obj.Add("w", Tater.CreateNumber("w", value.w));
			return obj;
		}
		private Tater Vector2IntToTater(string name, Vector2Int value) {
			Tater obj = Tater.CreateObject(name);
			obj.Add("x", Tater.CreateNumber("x", value.x));
			obj.Add("y", Tater.CreateNumber("y", value.y));
			return obj;
		}
		private Tater Vector3IntToTater(string name, Vector3Int value) {
			Tater obj = Tater.CreateObject(name);
			obj.Add("x", Tater.CreateNumber("x", value.x));
			obj.Add("y", Tater.CreateNumber("y", value.y));
			obj.Add("z", Tater.CreateNumber("z", value.z));
			return obj;
		}
		private Tater QuaternionToTater(string name, Quaternion value) {
			Tater obj = Tater.CreateObject(name);
			obj.Add("x", Tater.CreateNumber("x", value.x));
			obj.Add("y", Tater.CreateNumber("y", value.y));
			obj.Add("z", Tater.CreateNumber("z", value.z));
			obj.Add("w", Tater.CreateNumber("w", value.w));
			return obj;
		}
		private Tater RectToTater(string name, Rect value) {
			Tater obj = Tater.CreateObject(name);
			obj.Add("x", Tater.CreateNumber("x", value.x));
			obj.Add("y", Tater.CreateNumber("y", value.y));
			obj.Add("width", Tater.CreateNumber("width", value.width));
			obj.Add("height", Tater.CreateNumber("height", value.height));
			return obj;
		}
		private Tater RectIntToTater(string name, RectInt value) {
			Tater obj = Tater.CreateObject(name);
			obj.Add("x", Tater.CreateNumber("x", value.x));
			obj.Add("y", Tater.CreateNumber("y", value.y));
			obj.Add("width", Tater.CreateNumber("width", value.width));
			obj.Add("height", Tater.CreateNumber("height", value.height));
			return obj;
		}
		private Tater ColorToTater(string name, Color value) {
			Tater obj = Tater.CreateObject(name);
			obj.Add("r", Tater.CreateNumber("r", value.r));
			obj.Add("g", Tater.CreateNumber("g", value.g));
			obj.Add("b", Tater.CreateNumber("b", value.b));
			obj.Add("a", Tater.CreateNumber("a", value.a));
			return obj;
		}
		private Tater Color32ToTater(string name, Color32 value) {
			Tater obj = Tater.CreateObject(name);
			obj.Add("r", Tater.CreateNumber("r", value.r));
			obj.Add("g", Tater.CreateNumber("g", value.g));
			obj.Add("b", Tater.CreateNumber("b", value.b));
			obj.Add("a", Tater.CreateNumber("a", value.a));
			return obj;
		}


		#endregion

		#region IWriter


		public void Write(string name, IWritable value) {
			DoValue(name, value, ObjectToTater);
		}
		public void Write(string name, IEnumerable<IWritable> value) {
			DoCollection(name, value, ObjectToTater);
		}
		public void Write(string name, IDictionary<string, IWritable> value) {
			DoDictionary(name, value, ObjectToTater);
		}
		public void Write(string name, Enum value) {
			DoValue(name, value, EnumToTater);
		}
		public void Write(string name, IEnumerable<Enum> value) {
			DoCollection(name, value, EnumToTater);
		}
		public void Write(string name, IDictionary<string, Enum> value) {
			DoDictionary(name, value, EnumToTater);
		}

		#endregion


	}

}