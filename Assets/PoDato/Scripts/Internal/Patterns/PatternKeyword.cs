namespace PoDato {

	internal class PatternKeyword : IPattern {

		private readonly string m_keyword;

		public PatternKeyword(string keyword) {
			m_keyword = keyword;
		}

		public bool Matches(CharStream stream, out int length) {
			length = m_keyword.Length;
			if (!stream.IsEndOfFile(m_keyword.Length) && stream.Slice(m_keyword.Length) == m_keyword) {
				if (stream.IsEndOfFile(m_keyword.Length + 1)) {
					return true;
				} else {
					char peek = stream.Peek(m_keyword.Length + 1);
					if (!char.IsDigit(peek) && !char.IsLetter(peek) && peek != '_') {
						return true;
					}
				}
			}
			return false;
		}

	}

}