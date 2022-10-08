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
				throw new System.Exception(string.Format(
					"Unexpected end of file. Expecting " +
					"`{1}'", type
				));
			}
			if (Peek() != type) {
				throw new System.Exception(string.Format(
					"Unexpected token `{0}' at {1} -- " +
					"Expecting `{2}'.", Peek().Value, Peek().Position, type
				));
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