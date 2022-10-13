using UnityEditor;
using UnityEditor.AssetImporters;

namespace PoDato.Editor {

	[CustomEditor(typeof(TaterAssetImporter))]
	public class TaterAssetEditor : ScriptedImporterEditor {

		public override bool showImportedObject { get { return false; } }
		protected override bool needsApplyRevert { get { return false; } }
		protected override bool useAssetDrawPreview { get { return false; } }

		protected override void OnHeaderGUI() {
			base.OnHeaderGUI();
		}

		public override void OnInspectorGUI() {
			
			ApplyRevertGUI();
		}

	}

}


