using UnityEngine;

namespace PoDato {

	public class Test : MonoBehaviour {

		[SerializeField]
		private TaterAsset asset;

		private Lexer m_lexer = new Lexer(2);

		public void Awake() {
			m_lexer.Tokenize(asset.Text);
		}

	}

}