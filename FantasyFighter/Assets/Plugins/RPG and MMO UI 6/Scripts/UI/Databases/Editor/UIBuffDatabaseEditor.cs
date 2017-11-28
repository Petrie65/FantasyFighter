using UnityEngine;
using DuloGames.UI;
using UnityEditor;

namespace DuloGamesEditor.UI
{
    public class UIBuffDatabaseEditor
    {
        private static string GetSavePath()
        {
            return EditorUtility.SaveFilePanelInProject("New buff database", "New buff database", "asset", "Create a new buff database.");
        }

        [MenuItem("Assets/Create/Databases/Buff Database")]
        public static void CreateDatabase()
        {
            string assetPath = GetSavePath();
            UIBuffDatabase asset = ScriptableObject.CreateInstance("UIBuffDatabase") as UIBuffDatabase;  //scriptable object
            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
            AssetDatabase.Refresh();
        }
    }
}
