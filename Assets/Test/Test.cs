using PoDato;
using UnityEngine;

public class Test : MonoBehaviour {

	[SerializeField]
	private TaterAsset m_asset;

	private TaterReader m_reader;

	public void Awake() {
		m_reader = new TaterReader(2);
		ReadResult<Evidence> result = m_reader.Read<Evidence>(m_asset);
		Debug.Log("Finished Successfully");
	}

}