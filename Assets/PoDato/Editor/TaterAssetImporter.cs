using System.IO;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace PoDato.Editor {
	[ScriptedImporter(1, "tater")]
	public class TaterAssetImporter : ScriptedImporter {

		public override void OnImportAsset(AssetImportContext ctx) {

			TaterAsset asset = ScriptableObject.CreateInstance<TaterAsset>();
			asset.SetBytes(File.ReadAllBytes(ctx.assetPath));
			EditorUtility.SetDirty(asset);
			AssetDatabase.SaveAssets();

			ctx.AddObjectToAsset("text", asset);
			ctx.SetMainObject(asset);

		}

	}
}