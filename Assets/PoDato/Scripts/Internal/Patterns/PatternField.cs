using System;

namespace PoDato {

	internal class PatternField : IPattern {

		private const char CHAR_COLON = ':';
		private const char CHAR_UNDERSCORE = '_';

		public bool Matches(CharStream stream, out int length) {
			char current = stream.Peek();
			if (char.IsLetter(current) || current == CHAR_UNDERSCORE) {
				length = 1;
				while (!stream.IsEndOfFile(length)) {
					current = stream.Peek(length);
					if (current == CHAR_COLON || char.IsWhiteSpace(current)) {
						return true;
					}
					if (char.IsLetterOrDigit(current) || current == CHAR_UNDERSCORE) {
						length++;
					} else {
						throw new Exception($"Illegal character `{current}' in field name.");
					}
				}
				throw new Exception("Unexpected End of File");
			} else {
				throw new Exception($"First character of a field definition must be a letter, received `{current}'");
			}

		}


	}

}