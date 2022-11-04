using System;

namespace PoDato {

	internal class ParseException : Exception {
		public FilePosition Position { get; }
		public ParseException(FilePosition position, string message) : base(message) {
			Position = position;
		}
	}
	internal class DeserializationException : Exception {

		public Tater Tater { get; }		
		public DeserializationException(Tater tater, string message) : base(message) {
			Tater = tater;
		}

	}

}