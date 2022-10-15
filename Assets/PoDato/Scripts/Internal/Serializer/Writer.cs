using System;
using System.Collections.Generic;
using System.Text;

namespace PoDato {

	internal class Writer : IWriter {

		internal delegate Tater WriteFunc<T>(string name, T value);

		private StringBuilder m_builder;
		private Stack<Tater> m_output;
		private Stringifier m_stringifier;

		private Tater Current { get { return m_output.Peek(); } }

		public Writer(byte tabSize) {
			m_builder = new StringBuilder();
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
		private Tater BoolToTater(string name, bool value) {
			return Tater.CreateBoolean(name, value);
		}
		private Tater BoolProxyToTater(string name, IWriteProxy<bool> value) {
			return Tater.CreateBoolean(name, value.GetProxyValue());
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

		#endregion

	}

}