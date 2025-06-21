using UnityEngine;
using UnityEditor;
using System.IO;

public class PrefabAutoGenerator
{
    [MenuItem("Tools/Generate Prefabs from My Folders")]
    public static void GeneratePrefabs()
    {
        string[] folders = {
            "Assets/Books",
            "Assets/Bottel",
            "Assets/Chests",
            "Assets/Construction",
            "Assets/Decorations",
            "Assets/Door",
            "Assets/Furniture",
            "Assets/FurnitureDun",
            "Assets/Lights",
            "Assets/Models",
            "Assets/Plants",
            "Assets/Weapons"
        };

        string targetPath = "Assets/Resources/";

        if (!Directory.Exists(targetPath))
            Directory.CreateDirectory(targetPath);

        foreach (string sourcePath in folders)
        {
            if (!Directory.Exists(sourcePath))
            {
                Debug.LogWarning("Folder nie istnieje: " + sourcePath);
                continue;
            }

            string[] guids = AssetDatabase.FindAssets("t:Model", new[] { sourcePath });

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                GameObject model = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                if (model == null) continue;

                string prefabPath = Path.Combine(targetPath, model.name + ".prefab");

                PrefabUtility.SaveAsPrefabAsset(model, prefabPath);
                Debug.Log("✅ Prefab utworzony: " + prefabPath);
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("✅ Wszystko gotowe. Sprawdź folder Assets/Resources/");
    }
}
