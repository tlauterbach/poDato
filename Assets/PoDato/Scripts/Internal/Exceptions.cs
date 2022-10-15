using System;

namespace PoDato {

	internal class ParseException : Exception {
		internal ParseException(FilePosition pos, string message) : base($"pos{pos}: {message}") { 
		
		}
	}

}