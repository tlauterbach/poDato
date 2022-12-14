namespace PoDato {

	public class TaterReader {

		private Reader m_reader;

		public TaterReader(int tabSize = 2) {
			m_reader = new Reader(tabSize);
		}

		public ReadResult Read(string input) {
			return m_reader.Read(input);
		}
		public ReadResult Read(TaterAsset asset) {
			return m_reader.Read(asset.Text);
		}
		public ReadResult<T> Read<T>(string input) where T : IReadable, new() {
			return m_reader.Read<T>(input);
		}
		public ReadResult<T> Read<T>(TaterAsset asset) where T : IReadable, new() {
			return m_reader.Read<T>(asset.Text);
		}
		public ReadResult<T> Read<T>(Tater input) where T : IReadable, new() {
			return m_reader.Read<T>(input);
		}

	}

}


