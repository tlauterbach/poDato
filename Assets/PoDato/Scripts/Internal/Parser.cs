﻿namespace PoDato {

	internal class Parser {
		
		public Tater Parse(TokenStream stream) {
			Tater root;
			if (stream.IsEndOfFile()) {
				return null;
			} else if (stream.Peek(TokenType.OpenSquare)) {
				root = ParseArray(stream, "root");
			} else if (stream.Peek(TokenType.OpenCurly)) {
				root = ParseObject(stream, "root");
			} else {
				throw new ParseException(stream.Peek().Position, "Root value must be an object or array.");
			}
			return root;
		}

		private Tater ParseArray(TokenStream stream, string name) {
			int lineNumber = stream.Peek().Position.Line;
			stream.Expect(TokenType.OpenSquare);
			Tater array = Tater.CreateArray(name, lineNumber);
			int index = 0;
			while (!stream.IsEndOfFile() && !stream.Peek(TokenType.CloseSquare)) {
				array.Add(ParseValue(stream, $"{name}_{index++}"));
			}
			stream.Expect(TokenType.CloseSquare);
			return array;
		}

		private Tater ParseObject(TokenStream stream, string name) {
			int lineNumber = stream.Peek().Position.Line;
			stream.Expect(TokenType.OpenCurly);
			Tater obj = Tater.CreateObject(name, lineNumber);
			while (!stream.IsEndOfFile() && !stream.Peek(TokenType.CloseCurly)) {
				string valueName = RemoveQuotes(stream.Peek());
				stream.Expect(TokenType.Field);
				stream.Expect(TokenType.Colon);
				obj.Add(valueName, ParseValue(stream, valueName));
			}
			stream.Expect(TokenType.CloseCurly);
			return obj;
		}

		private Tater ParseValue(TokenStream stream, string name) {
			Token peek = stream.Peek();
			int lineNumber = peek.Position.Line;
			if (peek == TokenType.OpenCurly) {
				return ParseObject(stream, name);
			} else if (peek == TokenType.OpenSquare) {
				return ParseArray(stream, name);
			} else if (peek == TokenType.Number) {
				stream.Advance();
				if (double.TryParse(peek, out double value)) {
					return Tater.CreateNumber(name, value, lineNumber);
				} else {
					throw new ParseException(peek.Position, "Malformed number");
				}
			} else if (peek == TokenType.String) {
				stream.Advance();
				return Tater.CreateString(name, RemoveEscapedQuotes(RemoveQuotes(peek)), lineNumber);
			} else if (peek == TokenType.True) {
				stream.Advance();
				return Tater.CreateBoolean(name, true, lineNumber);
			} else if (peek == TokenType.False) {
				stream.Advance();
				return Tater.CreateBoolean(name, false, lineNumber);
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
		private static string RemoveEscapedQuotes(string value) {
			return value.Replace("\\\"", "\"");
		}
	}

}