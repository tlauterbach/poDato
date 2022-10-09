using UnityEngine;

namespace PoDato {

	public class Test : MonoBehaviour {

		[SerializeField]
		private TaterAsset asset;

		private Parser m_parser = new Parser();
		private Lexer m_lexer = new Lexer(2);

		public void Awake() {
			TokenStream stream = m_lexer.Tokenize(asset.Text);
			Tater tater = m_parser.Parse(stream);
		}

	}

}