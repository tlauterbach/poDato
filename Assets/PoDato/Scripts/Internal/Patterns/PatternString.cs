namespace PoDato {

	internal class PatternString : IPattern {

		private const char CHAR_UNDERSCORE = '_';
		private const char CHAR_QUOTE = '"';
		private const char CHAR_ESCAPE = '\\';
		private const char CHAR_LINE_FEED = '\n';
		private const string RESERVED = "\n#:{}[]";

		public bool Matches(CharStream stream, out int length) {
			char first = stream.Peek();
			if (first == CHAR_QUOTE) {
				// explicit string
				length = 1;
				while (!stream.IsEndOfFile(length) && stream.Peek() != CHAR_LINE_FEED) {
					char prev = stream.Peek(length - 1);
					char current = stream.Peek(length);
					if (current == CHAR_QUOTE && prev != CHAR_ESCAPE) {
						length++;
						return true;
					}
					length++;
				}
				throw new System.Exception(string.Format(
					"String was not properly closed with a double quote character"
				));
			} else {
				// non-explicit string?
				length = 1;
				int noWhitespace = 1;
				while (!stream.IsEndOfFile(length)) {
					char current = stream.Peek(length);
					if (IsReserved(current)) {
						length = noWhitespace;
						return true;
					}
					length++;
					if (!char.IsWhiteSpace(current)) {
						noWhitespace = length;
					}
				}
				return true;
			}
		}

		private bool IsReserved(char c) {
			foreach (char reserved in RESERVED) {
				if (c == reserved) {
					return true;
				}
			}
			return false;
		}

	}

}