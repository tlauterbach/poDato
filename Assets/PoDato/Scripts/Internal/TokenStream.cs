using System.Collections.Generic;

namespace PoDato {

	internal class TokenStream {

		private List<Token> m_input;
		private int m_index;

		public TokenStream(IEnumerable<Token> input) {
			m_input = new List<Token>(input);
		}

		public Token Peek() {
			return m_input[m_index];
		}
		public bool Peek(TokenType type) {
			return m_input[m_index] == type;
		}
		public void Expect(TokenType type) {
			if (IsEndOfFile()) {
				if (m_input.Count == 0) {
					throw new ParseException(new FilePosition(1, 1), "Unexpected end of file");
				} else {
					throw new ParseException(m_input[m_input.Count - 1].Position, "Unexpected end of file");
				}
			}
			if (Peek() != type) {
				throw new ParseException(Peek().Position, $"Unexpected token `{Peek().Value}'");
			}
			Advance();
		}
		public bool Accept(TokenType type) {
			if (Peek() == type) {
				Advance();
				return true;
			} else {
				return false;
			}
		}
		public void Advance(int distance = 1) {
			m_index += distance;
		}
		public bool IsEndOfFile(int distance = 0) {
			return m_index + distance >= m_input.Count;
		}


	}

}