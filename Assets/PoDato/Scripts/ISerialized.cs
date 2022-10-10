namespace PoDato {

	public interface IReadable {
		void Deserialize(IReader reader);
	}
	public interface IWritable {
		void Serialize(IWriter writer);
	}

}