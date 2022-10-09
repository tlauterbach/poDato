namespace PoDato {

	internal class Parser {
		
		public Tater Parse(TokenStream stream) {
			Tater root;
			if (stream.IsEndOfFile()) {
				return null;
			} else if (stream.Peek(TokenType.OpenSquare)) {
				root = ParseArray(stream);
			} else if (stream.Peek(TokenType.OpenCurly)) {
				root = ParseObject(stream);
			} else {
				throw new ParseException(stream.Peek().Position, "Root value must be an object or array.");
			}
			return root;
		}

		private Tater ParseArray(TokenStream stream) {
			stream.Expect(TokenType.OpenSquare);
			Tater array = Tater.CreateArray();
			while (!stream.IsEndOfFile() && !stream.Peek(TokenType.CloseSquare)) {
				array.Add(ParseValue(stream));
			}
			stream.Expect(TokenType.CloseSquare);
			return array;
		}

		private Tater ParseObject(TokenStream stream) {
			stream.Expect(TokenType.OpenCurly);
			Tater obj = Tater.CreateObject();
			while (!stream.IsEndOfFile() && !stream.Peek(TokenType.CloseCurly)) {
				string name = RemoveQuotes(stream.Peek());
				stream.Expect(TokenType.Field);
				stream.Expect(TokenType.Colon);
				obj.Add(name, ParseValue(stream));
			}
			stream.Expect(TokenType.CloseCurly);
			return obj;
		}

		private Tater ParseValue(TokenStream stream) {
			Token peek = stream.Peek();
			if (peek == TokenType.OpenCurly) {
				return ParseObject(stream);
			} else if (peek == TokenType.OpenSquare) {
				return ParseArray(stream);
			} else if (peek == TokenType.Number) {
				stream.Advance();
				if (double.TryParse(peek, out double value)) {
					return Tater.CreateNumber(value);
				} else {
					throw new ParseException(peek.Position, "Malformed number");
				}
			} else if (peek == TokenType.String) {
				stream.Advance();
				return Tater.CreateString(RemoveQuotes(peek));
			} else if (peek == TokenType.True) {
				stream.Advance();
				return Tater.CreateBoolean(true);
			} else if (peek == TokenType.False) {
				stream.Advance();
				return Tater.CreateBoolean(false);
			} else if (peek == TokenType.Null) {
				stream.Advance();
				return Tater.CreateNull();
			}
			throw new ParseException(peek.Position, $"Unexpected token `{peek.Value}'");
		}

		private static string RemoveQuotes(string value) {
			if (value.Length >= 2 && value[0] == '"' && value[value.Length - 1] == '"') {
				return value.Substring(1, value.Length - 2);
			} else {
				return value;
			}
		}
	}

}