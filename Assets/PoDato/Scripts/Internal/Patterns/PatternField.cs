using System;

namespace PoDato {

	internal class PatternField : IPattern {

		private const char CHAR_COLON = ':';
		private const char CHAR_UNDERSCORE = '_';
		private const char CHAR_NEWLINE = '\n';

		public bool Matches(CharStream stream, out int length) {
			// scan to make sure there is a colon after us
			// otherwise we are a string value
			int scan = 0;
			bool foundColon = false;
			while (!stream.IsEndOfFile(scan) && stream.Peek(scan) != CHAR_NEWLINE) {
				if (stream.Peek(scan) == CHAR_COLON) {
					foundColon = true;
					break;
				}
				scan++;
			}
			if (!foundColon) {
				length = 0;
				return false;
			}

			// now that we've confirmed we are in fact
			// a field, build us as such
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
				length = 0;
				return false;
			}

		}


	}

}