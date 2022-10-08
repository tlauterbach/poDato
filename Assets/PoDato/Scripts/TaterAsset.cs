using System.Text;
using UnityEngine;

namespace PoDato {

	public sealed class TaterAsset : ScriptableObject {

		public byte[] Bytes {
			get {
				return m_bytes;
			}
		}
		public string Text {
			get {
				return Decode(Bytes);
			}
		}
		[SerializeField, HideInInspector]
		private byte[] m_bytes;


		public void SetBytes(byte[] bytes) {
			m_bytes = bytes;
		}
		private static string Decode(byte[] bytes) {
			return Encoding.UTF8.GetString(bytes);
		}

	}

}