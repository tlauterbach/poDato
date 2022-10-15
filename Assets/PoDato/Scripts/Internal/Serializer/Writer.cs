using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoDato {

	internal class Writer : IWriter {

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
				return Tater.CreateNull(name);
			} else {
				Tater obj = Tater.CreateObject(name);
				m_output.Push(obj);
				value.Serialize(this);
				m_output.Pop();
				return obj;
			}
		}
		private Tater EnumToTater(string name, Enum value) {
			return Tater.CreateString(name, value.ToString());
		}
		private Tater StringToTater(string name, string value) {
			if (string.IsNullOrEmpty(value)) {
				return Tater.CreateNull(name);
			} else {
				return Tater.CreateString(name, value);
			}
		}
		private Tater StringProxyToTater(string name, IWriteProxy<string> value) {
			string str = value.GetProxyValue();
			if (string.IsNullOrEmpty(str)) {
				return Tater.CreateNull(name);
			} else {
				return Tater.CreateString(name, str);
			}
		}
		private Tater Int32ToTater(string name, int value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater Int32ProxyToTater(string name, IWriteProxy<int> value) {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater UInt32ToTater(string name, uint value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater UInt32ProxyToTater(string name, IWriteProxy<uint> value) {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater Int16ToTater(string name, short value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater Int16ProxyToTater(string name, IWriteProxy<short> value) {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater UInt16ToTater(string name, ushort value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater UInt16ProxyToTater(string name, IWriteProxy<ushort> value) {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater Int64ToTater(string name, long value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater Int64ProxyToTater(string name, IWriteProxy<long> value) {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater UInt64ToTater(string name, ulong value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater UInt64ProxyToTater(string name, IWriteProxy<ulong> value) {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater DoubleToTater(string name, double value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater DoubleProxyToTater(string name, IWriteProxy<double> value) {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater SingleToTater(string name, float value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater SingleProxyToTater(string name, IWriteProxy<float> value) {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater ByteToTater(string name, byte value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater ByteProxyToTater(string name, IWriteProxy<byte> value) {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater SByteToTater(string name, sbyte value) {
			return Tater.CreateNumber(name, value);
		}
		private Tater SByteProxyToTater(string name, IWriteProxy<sbyte> value) {
			return Tater.CreateNumber(name, value.GetProxyValue());
		}
		private Tater BoolToTater(string name, bool value) {
			return Tater.CreateBoolean(name, value);
		}
		private Tater BoolProxyToTater(string name, IWriteProxy<bool> value) {
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

		#region IWritable

		public void Write(string name, IWritable value) {
			DoValue(name, value, ObjectToTater);
		}
		public void Write(string name, IEnumerable<IWritable> value) {
			DoCollection(name, value, ObjectToTater);
		}
		public void Write(string name, IDictionary<string, IWritable> value) {
			DoDictionary(name, value, ObjectToTater);
		}

		#endregion

		#region enum

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

		#region string

		public void Write(string name, string value) {
			DoValue(name, value, StringToTater);
		}

		public void Write(string name, IEnumerable<string> value) {
			DoCollection(name, value, StringToTater);
		}

		public void Write(string name, IDictionary<string, string> value) {
			DoDictionary(name, value, StringToTater);
		}

		public void Write(string name, IWriteProxy<string> value) {
			DoValue(name, value, StringProxyToTater);
		}

		public void Write(string name, IEnumerable<IWriteProxy<string>> value) {
			DoCollection(name, value, StringProxyToTater);
		}

		public void Write(string name, IDictionary<string, IWriteProxy<string>> value) {
			DoDictionary(name, value, StringProxyToTater);
		}

		#endregion

		#region int

		public void Write(string name, int value) {
			DoValue(name, value, Int32ToTater);
		}

		public void Write(string name, IEnumerable<int> value) {
			DoCollection(name, value, Int32ToTater);
		}

		public void Write(string name, IDictionary<string, int> value) {
			DoDictionary(name, value, Int32ToTater);
		}

		public void Write(string name, IWriteProxy<int> value) {
			DoValue(name, value, Int32ProxyToTater);
		}

		public void Write(string name, IEnumerable<IWriteProxy<int>> value) {
			DoCollection(name, value, Int32ProxyToTater);
		}

		public void Write(string name, IDictionary<string, IWriteProxy<int>> value) {
			DoDictionary(name, value, Int32ProxyToTater);
		}

		#endregion

		#region uint

		public void Write(string name, uint value) {
			DoValue(name, value, UInt32ToTater);
		}

		public void Write(string name, IEnumerable<uint> value) {
			DoCollection(name, value, UInt32ToTater);
		}

		public void Write(string name, IDictionary<string, uint> value) {
			DoDictionary(name, value, UInt32ToTater);
		}

		public void Write(string name, IWriteProxy<uint> value) {
			DoValue(name, value, UInt32ProxyToTater);
		}

		public void Write(string name, IEnumerable<IWriteProxy<uint>> value) {
			DoCollection(name, value, UInt32ProxyToTater);
		}

		public void Write(string name, IDictionary<string, IWriteProxy<uint>> value) {
			DoDictionary(name, value, UInt32ProxyToTater);
		}

		#endregion

		#region short

		public void Write(string name, short value) {
			DoValue(name, value, Int16ToTater);
		}

		public void Write(string name, IEnumerable<short> value) {
			DoCollection(name, value, Int16ToTater);
		}

		public void Write(string name, IDictionary<string, short> value) {
			DoDictionary(name, value, Int16ToTater);
		}

		public void Write(string name, IWriteProxy<short> value) {
			DoValue(name, value, Int16ProxyToTater);
		}

		public void Write(string name, IEnumerable<IWriteProxy<short>> value) {
			DoCollection(name, value, Int16ProxyToTater);
		}

		public void Write(string name, IDictionary<string, IWriteProxy<short>> value) {
			DoDictionary(name, value, Int16ProxyToTater);
		}

		#endregion

		#region ushort

		public void Write(string name, ushort value) {
			DoValue(name, value, UInt16ToTater);
		}

		public void Write(string name, IEnumerable<ushort> value) {
			DoCollection(name, value, UInt16ToTater);
		}

		public void Write(string name, IDictionary<string, ushort> value) {
			DoDictionary(name, value, UInt16ToTater);
		}

		public void Write(string name, IWriteProxy<ushort> value) {
			DoValue(name, value, UInt16ProxyToTater);
		}

		public void Write(string name, IEnumerable<IWriteProxy<ushort>> value) {
			DoCollection(name, value, UInt16ProxyToTater);
		}

		public void Write(string name, IDictionary<string, IWriteProxy<ushort>> value) {
			DoDictionary(name, value, UInt16ProxyToTater);
		}

		#endregion

		#region long

		public void Write(string name, long value) {
			DoValue(name, value, Int64ToTater);
		}

		public void Write(string name, IEnumerable<long> value) {
			DoCollection(name, value, Int64ToTater);
		}

		public void Write(string name, IDictionary<string, long> value) {
			DoDictionary(name, value, Int64ToTater);
		}

		public void Write(string name, IWriteProxy<long> value) {
			DoValue(name, value, Int64ProxyToTater);
		}

		public void Write(string name, IEnumerable<IWriteProxy<long>> value) {
			DoCollection(name, value, Int64ProxyToTater);
		}

		public void Write(string name, IDictionary<string, IWriteProxy<long>> value) {
			DoDictionary(name, value, Int64ProxyToTater);
		}

		#endregion

		#region ulong

		public void Write(string name, ulong value) {
			DoValue(name, value, UInt64ToTater);
		}

		public void Write(string name, IEnumerable<ulong> value) {
			DoCollection(name, value, UInt64ToTater);
		}

		public void Write(string name, IDictionary<string, ulong> value) {
			DoDictionary(name, value, UInt64ToTater);
		}

		public void Write(string name, IWriteProxy<ulong> value) {
			DoValue(name, value, UInt64ProxyToTater);
		}

		public void Write(string name, IEnumerable<IWriteProxy<ulong>> value) {
			DoCollection(name, value, UInt64ProxyToTater);
		}

		public void Write(string name, IDictionary<string, IWriteProxy<ulong>> value) {
			DoDictionary(name, value, UInt64ProxyToTater);
		}

		#endregion

		#region byte

		public void Write(string name, byte value) {
			DoValue(name, value, ByteToTater);
		}

		public void Write(string name, IEnumerable<byte> value) {
			DoCollection(name, value, ByteToTater);
		}

		public void Write(string name, IDictionary<string, byte> value) {
			DoDictionary(name, value, ByteToTater);
		}

		public void Write(string name, IWriteProxy<byte> value) {
			DoValue(name, value, ByteProxyToTater);
		}

		public void Write(string name, IEnumerable<IWriteProxy<byte>> value) {
			DoCollection(name, value, ByteProxyToTater);
		}

		public void Write(string name, IDictionary<string, IWriteProxy<byte>> value) {
			DoDictionary(name, value, ByteProxyToTater);
		}

		#endregion

		#region ulong

		public void Write(string name, sbyte value) {
			DoValue(name, value, SByteToTater);
		}

		public void Write(string name, IEnumerable<sbyte> value) {
			DoCollection(name, value, SByteToTater);
		}

		public void Write(string name, IDictionary<string,sbyte> value) {
			DoDictionary(name, value, SByteToTater);
		}

		public void Write(string name, IWriteProxy<sbyte> value) {
			DoValue(name, value, SByteProxyToTater);
		}

		public void Write(string name, IEnumerable<IWriteProxy<sbyte>> value) {
			DoCollection(name, value, SByteProxyToTater);
		}

		public void Write(string name, IDictionary<string, IWriteProxy<sbyte>> value) {
			DoDictionary(name, value, SByteProxyToTater);
		}

		#endregion

		#region bool

		public void Write(string name, bool value) {
			DoValue(name, value, BoolToTater);
		}

		public void Write(string name, IEnumerable<bool> value) {
			DoCollection(name, value, BoolToTater);
		}

		public void Write(string name, IDictionary<string, bool> value) {
			DoDictionary(name, value, BoolToTater);
		}

		public void Write(string name, IWriteProxy<bool> value) {
			DoValue(name, value, BoolProxyToTater);
		}

		public void Write(string name, IEnumerable<IWriteProxy<bool>> value) {
			DoCollection(name, value, BoolProxyToTater);
		}

		public void Write(string name, IDictionary<string, IWriteProxy<bool>> value) {
			DoDictionary(name, value, BoolProxyToTater);
		}

		#endregion

		#region double

		public void Write(string name, double value) {
			DoValue(name, value, DoubleToTater);
		}

		public void Write(string name, IEnumerable<double> value) {
			DoCollection(name, value, DoubleToTater);
		}

		public void Write(string name, IDictionary<string, double> value) {
			DoDictionary(name, value, DoubleToTater);
		}

		public void Write(string name, IWriteProxy<double> value) {
			DoValue(name, value, DoubleProxyToTater);
		}

		public void Write(string name, IEnumerable<IWriteProxy<double>> value) {
			DoCollection(name, value, DoubleProxyToTater);
		}

		public void Write(string name, IDictionary<string, IWriteProxy<double>> value) {
			DoDictionary(name, value, DoubleProxyToTater);
		}

		#endregion

		#region float

		public void Write(string name, float value) {
			DoValue(name, value, SingleToTater);
		}

		public void Write(string name, IEnumerable<float> value) {
			DoCollection(name, value, SingleToTater);
		}

		public void Write(string name, IDictionary<string, float> value) {
			DoDictionary(name, value, SingleToTater);
		}

		public void Write(string name, IWriteProxy<float> value) {
			DoValue(name, value, SingleProxyToTater);
		}

		public void Write(string name, IEnumerable<IWriteProxy<float>> value) {
			DoCollection(name, value, SingleProxyToTater);
		}

		public void Write(string name, IDictionary<string, IWriteProxy<float>> value) {
			DoDictionary(name, value, SingleProxyToTater);
		}

		#endregion

		#region Vector2

		public void Write(string name, Vector2 value) {
			DoValue(name, value, Vector2ToTater);
		}

		public void Write(string name, IEnumerable<Vector2> value) {
			DoCollection(name, value, Vector2ToTater);
		}

		public void Write(string name, IDictionary<string, Vector2> value) {
			DoDictionary(name, value, Vector2ToTater);
		}

		#endregion

		#region Vector3

		public void Write(string name, Vector3 value) {
			DoValue(name, value, Vector3ToTater);
		}

		public void Write(string name, IEnumerable<Vector3> value) {
			DoCollection(name, value, Vector3ToTater);
		}

		public void Write(string name, IDictionary<string, Vector3> value) {
			DoDictionary(name, value, Vector3ToTater);
		}

		#endregion

		#region Vector4

		public void Write(string name, Vector4 value) {
			DoValue(name, value, Vector4ToTater);
		}

		public void Write(string name, IEnumerable<Vector4> value) {
			DoCollection(name, value, Vector4ToTater);
		}

		public void Write(string name, IDictionary<string, Vector4> value) {
			DoDictionary(name, value, Vector4ToTater);
		}

		#endregion

		#region Vector2Int

		public void Write(string name, Vector2Int value) {
			DoValue(name, value, Vector2IntToTater);
		}

		public void Write(string name, IEnumerable<Vector2Int> value) {
			DoCollection(name, value, Vector2IntToTater);
		}

		public void Write(string name, IDictionary<string, Vector2Int> value) {
			DoDictionary(name, value, Vector2IntToTater);
		}

		#endregion

		#region Vector3Int

		public void Write(string name, Vector3Int value) {
			DoValue(name, value, Vector3IntToTater);
		}

		public void Write(string name, IEnumerable<Vector3Int> value) {
			DoCollection(name, value, Vector3IntToTater);
		}

		public void Write(string name, IDictionary<string, Vector3Int> value) {
			DoDictionary(name, value, Vector3IntToTater);
		}

		#endregion

		#region Quaternion

		public void Write(string name, Quaternion value) {
			DoValue(name, value, QuaternionToTater);
		}

		public void Write(string name, IEnumerable<Quaternion> value) {
			DoCollection(name, value, QuaternionToTater);
		}

		public void Write(string name, IDictionary<string, Quaternion> value) {
			DoDictionary(name, value, QuaternionToTater);
		}

		#endregion

		#region Color

		public void Write(string name, Color value) {
			DoValue(name, value, ColorToTater);
		}

		public void Write(string name, IEnumerable<Color> value) {
			DoCollection(name, value, ColorToTater);
		}

		public void Write(string name, IDictionary<string, Color> value) {
			DoDictionary(name, value, ColorToTater);
		}

		#endregion

		#region Color32

		public void Write(string name, Color32 value) {
			DoValue(name, value, Color32ToTater);
		}

		public void Write(string name, IEnumerable<Color32> value) {
			DoCollection(name, value, Color32ToTater);
		}

		public void Write(string name, IDictionary<string, Color32> value) {
			DoDictionary(name, value, Color32ToTater);
		}

		#endregion

		#region Rect

		public void Write(string name, Rect value) {
			DoValue(name, value, RectToTater);
		}

		public void Write(string name, IEnumerable<Rect> value) {
			DoCollection(name, value, RectToTater);
		}

		public void Write(string name, IDictionary<string, Rect> value) {
			DoDictionary(name, value, RectToTater);
		}

		#endregion

		#region RectInt

		public void Write(string name, RectInt value) {
			DoValue(name, value, RectIntToTater);
		}

		public void Write(string name, IEnumerable<RectInt> value) {
			DoCollection(name, value, RectIntToTater);
		}

		public void Write(string name, IDictionary<string, RectInt> value) {
			DoDictionary(name, value, RectIntToTater);
		}

		#endregion

		#endregion

	}

}