using System.Collections.Generic;

namespace PoDato {

	public class TaterWriter {

		private Writer m_writer;

		public TaterWriter(byte tabSize) {
			m_writer = new Writer(tabSize);
		}

		public string Write(IWritable value) {
			return m_writer.Write(value);
		}
		public string Write(IEnumerable<IWritable> value) {
			return m_writer.Write(value);
		}
		public string Write(Tater tater) {
			return m_writer.Write(tater);
		}


	}

}