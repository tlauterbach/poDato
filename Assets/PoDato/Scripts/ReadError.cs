namespace PoDato {

	public class ReadError {

		/// <summary>
		/// Was the error thrown during parsing (true) or during deserialization (false)
		/// </summary>
		public bool IsParseError { get; }
		/// <summary>
		/// Was the error thrown during deserialization (true) or during parsing (false)
		/// </summary>
		public bool IsDeserializationError { get; }
		/// <summary>
		/// Preformatted, entire message body of the ReadError
		/// </summary>
		public string FullMessage { get; }
		/// <summary>
		/// Partial message body of the ReadError
		/// </summary>
		public string Message { get; }
		/// <summary>
		/// Referential path of the Tater object that threw the error. Empty if IsParseError
		/// </summary>
		public string Path { get; }
		/// <summary>
		/// Referential line number of the Tater object that threw the error
		/// </summary>
		public int LineNumber { get; }
		/// <summary>
		/// Horizontal position of the file that the error occured at. -1 if Deserialization error
		/// </summary>
		public int LinePosition { get; }

		public static implicit operator string(ReadError error) {
			return error.FullMessage;
		}

		internal ReadError(ParseException exception) {
			Message = exception.Message;
			LineNumber = exception.Position.Line;
			LinePosition = exception.Position.Position;
			Path = string.Empty;
			IsParseError = true;
			IsDeserializationError = false;
			FullMessage = $"({LineNumber},{LinePosition}) {Message}";
		}
		internal ReadError(DeserializationException exception, string path) {
			Message = exception.Message;
			LineNumber = exception.Tater.LineNumber;
			LinePosition = -1;
			Path = path;
			IsParseError = false;
			IsDeserializationError = true;
			FullMessage = $"(line {LineNumber}) {Path}: {Message}";
		}
		internal ReadError(string message, int lineNumber, string path) {
			Message = message;
			LineNumber = lineNumber;
			LinePosition = -1;
			Path = path;
			IsParseError = false;
			IsDeserializationError = true;
			FullMessage = $"(line {LineNumber}) {Path}: {Message}";
		}

		public override string ToString() {
			return FullMessage;
		}

	}

}