namespace PoDato {

	public interface ISerialized {

		void OnSerialize(IWriter writer);
		void OnDeserialize(IReader reader);

	}

}