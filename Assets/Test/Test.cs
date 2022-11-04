using PoDato;
using System.Text;
using UnityEngine;

public class Test : MonoBehaviour {

	[SerializeField]
	private TaterAsset m_evidence;
	[SerializeField]
	private TaterAsset m_manyTypes;

	private TaterReader m_reader;
	private TaterWriter m_writer;

	public void Awake() {
		m_reader = new TaterReader(2);
		ReadResult<Evidence> evidence = m_reader.Read<Evidence>(m_evidence);
		if (evidence.IsSuccess) {
			Debug.Log($"Read Successful: {m_evidence.name}");
			m_writer = new TaterWriter(2);
			string output = m_writer.Write(evidence.ResultArray);
			Debug.Log(output);
		} else {
			LogErrors(evidence);
		}
		ReadResult<ManyTypes> manyTypes = m_reader.Read<ManyTypes>(m_manyTypes);
		if (manyTypes.IsSuccess) {
			Debug.Log($"Read Successful: {m_manyTypes.name}");
			m_writer = new TaterWriter(2);
			string output = m_writer.Write(manyTypes.ResultObject);
			Debug.Log(output);
		} else {
			LogErrors(evidence);
		}

	}

	private void LogErrors<T>(ReadResult<T> result) where T : IReadable, new() {
		StringBuilder builder = new StringBuilder();
		foreach (ReadError error in result.Errors) {
			builder.Append(error.FullMessage).Append('\n');
		}
		builder.Length--;
		Debug.LogWarning(builder.ToString());
	}

}