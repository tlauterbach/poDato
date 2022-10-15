using PoDato;
using System.Text;
using UnityEngine;

public class Test : MonoBehaviour {

	[SerializeField]
	private TaterAsset m_asset;

	private TaterReader m_reader;
	private TaterWriter m_writer;

	public void Awake() {
		m_reader = new TaterReader(2);
		ReadResult<Evidence> result = m_reader.Read<Evidence>(m_asset);
		if (result.IsSuccess) {
			Debug.Log("Read Successful");
			m_writer = new TaterWriter(2);
			string output = m_writer.Write(result.ResultArray);
			Debug.Log(output);
		} else {
			StringBuilder builder = new StringBuilder();
			bool isFirst = true;
			for (int ix = 0; ix < result.Errors.Count; ix++) {
				if (isFirst) {
					isFirst = false;
				} else {
					builder.Append('\n');
				}
				builder.Append(result.Errors[ix]);
			}
			Debug.LogWarning(builder.ToString());
		}
		
	}

}