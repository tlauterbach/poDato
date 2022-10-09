using System;

namespace PoDato {

	public class ParseException : Exception {
		internal ParseException(FilePosition pos, string message) : base($"pos{pos}: {message}") { 
		
		}
	}

}