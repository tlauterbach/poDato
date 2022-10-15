using System;
using System.Collections.Generic;

namespace PoDato {

	public interface IWriter {

		// IWritable
		void Write(string name, IWritable value);
		void Write(string name, IEnumerable<IWritable> value);
		void Write(string name, IDictionary<string, IWritable> value);

		// enum
		void Write(string name, Enum value);
		void Write(string name, IEnumerable<Enum> value);
		void Write(string name, IDictionary<string, Enum> value);

		// string
		void Write(string name, string value);
		void Write(string name, IEnumerable<string> value);
		void Write(string name, IDictionary<string, string> value);
		void Write(string name, IWriteProxy<string> value);
		void Write(string name, IEnumerable<IWriteProxy<string>> value);
		void Write(string name, IDictionary<string, IWriteProxy<string>> value);

		// int
		void Write(string name, int value);
		void Write(string name, IEnumerable<int> value);
		void Write(string name, IDictionary<string, int> value);
		void Write(string name, IWriteProxy<int> value);
		void Write(string name, IEnumerable<IWriteProxy<int>> value);
		void Write(string name, IDictionary<string, IWriteProxy<int>> value);

		// uint
		void Write(string name, uint value);
		void Write(string name, IEnumerable<uint> value);
		void Write(string name, IDictionary<string, uint> value);
		void Write(string name, IWriteProxy<uint> value);
		void Write(string name, IEnumerable<IWriteProxy<uint>> value);
		void Write(string name, IDictionary<string, IWriteProxy<uint>> value);

		// short
		void Write(string name, short value);
		void Write(string name, IEnumerable<short> value);
		void Write(string name, IDictionary<string, short> value);
		void Write(string name, IWriteProxy<short> value);
		void Write(string name, IEnumerable<IWriteProxy<short>> value);
		void Write(string name, IDictionary<string, IWriteProxy<short>> value);

		// ushort
		void Write(string name, ushort value);
		void Write(string name, IEnumerable<ushort> value);
		void Write(string name, IDictionary<string, ushort> value);
		void Write(string name, IWriteProxy<ushort> value);
		void Write(string name, IEnumerable<IWriteProxy<ushort>> value);
		void Write(string name, IDictionary<string, IWriteProxy<ushort>> value);

		// bool
		void Write(string name, bool value);
		void Write(string name, IEnumerable<bool> value);
		void Write(string name, IDictionary<string, bool> value);
		void Write(string name, IWriteProxy<bool> value);
		void Write(string name, IEnumerable<IWriteProxy<bool>> value);
		void Write(string name, IDictionary<string, IWriteProxy<bool>> value);


	}

}