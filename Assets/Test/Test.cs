using PoDato;
using System.Text;
using UnityEngine;

public class Test : MonoBehaviour {

	[SerializeField]
	private TaterAsset m_asset;

	private TaterReader m_reader;

	public void Awake() {
		m_reader = new TaterReader(2);
		ReadResult<ManyTypes> result = m_reader.Read<ManyTypes>(m_asset);
		if (result.IsSuccess) {
			Debug.Log("Finished Successfully");
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