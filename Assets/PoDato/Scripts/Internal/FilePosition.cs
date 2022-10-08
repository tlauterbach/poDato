namespace PoDato {

	internal struct FilePosition {

		public int Line { get; private set; }
		public int Position { get; private set; }

		private const string STRING_FORMAT = "({0},{1})";

		public FilePosition(int line, int position) {
			Line = line;
			Position = position;
		}
		public FilePosition Tab(int tabSize) {
			return new FilePosition(Line, Position + tabSize);
		}
		public FilePosition Space() {
			return new FilePosition(Line, Position + 1);
		}
		public FilePosition LineFeed() {
			return new FilePosition(Line + 1, 1);
		}
		public FilePosition Advance(int distance) {
			return new FilePosition(Line, Position + distance);
		}

		public override string ToString() {
			return string.Format(STRING_FORMAT, Line, Position);
		}

	}

}