using System;

namespace PoDato {

	internal class CharStream {

		public int Index { get; private set; }

		public FilePosition Position { get; private set; }

		private string m_input;
		private int m_tabSize;


		public CharStream(string input, int tabSize = 4) {
			Position = new FilePosition(1, 1);
			Index = 0;
			m_input = input;
			m_tabSize = tabSize;
		}

		public void Reset(string input, int tabSize = 4) {
			Position = new FilePosition(1, 1);
			Index = 0;
			m_input = input;
			m_tabSize = tabSize;
		}

		public char Peek() {
			return m_input[Index];
		}
		public char Peek(int distance) {
			if (IsEndOfFile(distance)) {
				throw new IndexOutOfRangeException();
			}
			return m_input[Index + distance];
		}
		public StringSlice Slice(int length) {
			return new StringSlice(m_input, Index, length);
		}
		public void TabPosition() {
			Position = Position.Tab(m_tabSize);
		}
		public void SpacePosition() {
			Position = Position.Space();
		}
		public void LineFeedPosition() {
			Position = Position.LineFeed();
		}
		public void AdvancePosition(int distance = 1) {
			Position = Position.Advance(distance);
		}

		public void AdvanceIndex(int distance = 1) {
			Index += distance;
		}

		public bool IsEndOfFile(int distance = 0) {
			return Index + distance >= m_input.Length;
		}

	}

}