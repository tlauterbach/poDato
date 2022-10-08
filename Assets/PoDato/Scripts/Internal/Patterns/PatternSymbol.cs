namespace PoDato {

	internal class PatternSymbol : IPattern {

		private readonly string m_symbol;

		public PatternSymbol(string symbol) {
			m_symbol = symbol;
		}
		public bool Matches(CharStream stream, out int length) {
			length = m_symbol.Length;
			return !stream.IsEndOfFile(m_symbol.Length-1) &&
				stream.Slice(m_symbol.Length) == m_symbol;
		}

	}

}