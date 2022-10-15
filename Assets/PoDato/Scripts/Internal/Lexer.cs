using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoDato {

	internal class Lexer {

		private const char COMMENT = '#';

		private static readonly Dictionary<TokenType, IPattern> m_symbols = new Dictionary<TokenType, IPattern>() {
			{ TokenType.OpenCurly, new PatternSymbol("{") },
			{ TokenType.CloseCurly, new PatternSymbol("}") },
			{ TokenType.OpenSquare, new PatternSymbol("[") },
			{ TokenType.CloseSquare, new PatternSymbol("]") },
			{ TokenType.Colon, new PatternSymbol(":") }
		};
		private static readonly PatternField m_field = new PatternField();
		private static readonly Dictionary<TokenType, IPattern> m_values = new Dictionary<TokenType, IPattern>() {
			{ TokenType.True, new PatternKeyword("true") },
			{ TokenType.False, new PatternKeyword("false") },
			{ TokenType.Null, new PatternKeyword("null") },
			{ TokenType.Number, new PatternNumber() },
			{ TokenType.String, new PatternString() }
		};

		private CharStream m_stream;
		private List<Token> m_tokens;
		private int m_tabSize;
		
		public Lexer(int tabSize) {
			m_stream = new CharStream(string.Empty, tabSize);
			m_tabSize = tabSize;
			m_tokens = new List<Token>();
		}

		public TokenStream Tokenize(string input) {
			input = input.Replace("\r\n", "\n").Replace("\r", "\n");

			m_stream.Reset(input, m_tabSize);
			m_tokens.Clear();
			while (!m_stream.IsEndOfFile()) {
				try {
					// check if current is space or tab
					if (TryIgnoreWhiteSpace()) {
						continue;
					}
					// check if current is a comment
					if (TryHandleComment()) {
						continue;
					}
					// check if current is control symbol
					if (TraverseDictionary(m_symbols)) {
						continue;
					}
					if (TryAddToken(TokenType.Field, m_field)) {
						continue;
					}
					if (TraverseDictionary(m_values)) {
						continue;
					}
				} catch (Exception e) {
					throw new Exception(string.Format("at {0}: {1}", m_stream.Position, e.Message));
				}
				throw new Exception(string.Format(
					"Invalid character `{0}' at {1} in input string",
					m_stream.Peek(), m_stream.Position
				));
			}
			return new TokenStream(m_tokens);
		}

		private bool TryIgnoreWhiteSpace() {
			char c = m_stream.Peek();
			switch (c) {
				case '\t': 
					m_stream.TabPosition(); 
					m_stream.AdvanceIndex(); 
					return true;
				case ' ':
					m_stream.SpacePosition(); 
					m_stream.AdvanceIndex(); 
					return true;
				case '\n': 
					m_stream.LineFeedPosition(); 
					m_stream.AdvanceIndex();  
					return true;
				default: 
					return false;
			}
		}
		private bool TryHandleComment() {
			if (m_stream.Peek() == COMMENT) {
				while (!m_stream.IsEndOfFile() && m_stream.Peek() != '\n') {
					m_stream.AdvanceIndex();
				}
				return true;
			} else {
				return false;
			}
		}

		private bool TraverseDictionary<T>(IDictionary<TokenType, T> dictionary) where T : IPattern {
			foreach (TokenType type in dictionary.Keys) {
				if (TryAddToken(type, dictionary[type])) {
					return true;
				}
			}
			return false;
		}

		private bool TryAddToken(TokenType type, IPattern pattern) {
			if (pattern.Matches(m_stream, out int length)) {
				Token token = new Token(type, m_stream.Position, m_stream.Slice(length));
				m_stream.AdvanceIndex(length);
				m_stream.AdvancePosition(length);
				m_tokens.Add(token);
				return true;
			} else {
				return false;
			}
		}
	}

}