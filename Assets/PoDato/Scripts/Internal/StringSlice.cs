using System;
using System.Collections;
using System.Collections.Generic;

namespace PoDato {

	/// <summary>
	/// A string subsection that doesn't create 
	/// a copy of the source string, reducing 
	/// garbage for string heavy operations.
	/// </summary>
	internal struct StringSlice : IEquatable<StringSlice>, IEquatable<string>, IEnumerable<char> {

		public static readonly StringSlice Empty = new StringSlice();

		public char this[int index] {
			get {
				if (index < 0 || index >= Length) {
					throw new IndexOutOfRangeException();
				}
				return m_source[m_index + index];
			}
		}
		public int Length { get; }

		private readonly string m_source;
		private readonly int m_index;

		public StringSlice(string source) {
			// use the whole string
			m_source = source;
			m_index = 0;
			Length = source.Length;
		}
		public StringSlice(string source, int index) {
			// use the string starting at index
			m_source = source;
			m_index = Math.Min(Math.Max(index, 0), m_source.Length - 1);
			Length = source.Length - index;
		}
		public StringSlice(string source, int index, int length) {
			// use the string starting at index for length number of characers
			m_source = source;
			m_index = Math.Min(Math.Max(index, 0), m_source.Length - 1);
			Length = Math.Min(length, source.Length - m_index);
		}

		public static bool operator ==(StringSlice lhs, StringSlice rhs) {
			return lhs.Equals(rhs);
		}
		public static bool operator !=(StringSlice lhs, StringSlice rhs) {
			return !lhs.Equals(rhs);
		}
		public static bool operator ==(StringSlice lhs, string rhs) {
			return lhs.Equals(rhs);
		}
		public static bool operator !=(StringSlice lhs, string rhs) {
			return !lhs.Equals(rhs);
		}

		public bool Equals(StringSlice other) {
			if (Length != other.Length) {
				return false;
			}
			return other.GetHashCode() == GetHashCode();
		}

		public bool Equals(string other) {
			if (Length != other.Length) {
				return false;
			}
			for (int ix = 0; ix < Length; ix++) {
				if (this[ix] != other[ix]) {
					return false;
				}
			}
			return true;
		}

		public override bool Equals(object obj) {
			if (obj == null) {
				return false;
			} else if (obj is StringSlice) {
				return Equals((StringSlice)obj);
			}
			string asString = obj as string;
			if (asString == null) {
				return false;
			} else {
				return Equals(asString);
			}
		}

		public override int GetHashCode() {
			if (Length <= 0) {
				return string.Empty.GetHashCode();
			} else if (m_index == 0 && Length == m_source.Length) {
				return m_source.GetHashCode();
			}
			int hash = 9551;
			for (int ix = 0; ix < Length; ix++) {
				ushort val = Convert.ToUInt16(m_source[m_index + ix]);
				hash = (hash * 13 + 37 * val) % int.MaxValue;
			}
			return hash;
		}

		public override string ToString() {
			if (Length <= 0) {
				return string.Empty;
			} else if (m_index == 0 && Length == m_source.Length) {
				return m_source;
			} else {
				return m_source.Substring(m_index, Length);
			}
		}

		public IEnumerator<char> GetEnumerator() {
			for (int ix = m_index; ix < Length; ix++) {
				yield return m_source[ix];
			}
		}

		IEnumerator IEnumerable.GetEnumerator() {
			for (int ix = m_index; ix < Length; ix++) {
				yield return m_source[ix];
			}
		}
	}


}