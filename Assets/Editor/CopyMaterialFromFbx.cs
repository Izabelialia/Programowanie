using UnityEngine;
using UnityEditor;
using System.IO;

public class CopyMaterialFromFbx
{
    [MenuItem("Tools/Copy Material from FBX to Prefab")]
    public static void FixPrefabs()
    {
        string prefabFolder = "Assets/Resources/";
        string modelFolder = "Assets/"; // Przeszukamy cały projekt

        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { prefabFolder });

        int fixedCount = 0;

        foreach (string prefabGuid in prefabGuids)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(prefabGuid);
            string prefabName = Path.GetFileNameWithoutExtension(prefabPath);

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab == null) continue;

            MeshRenderer prefabRenderer = prefab.GetComponentInChildren<MeshRenderer>();
            if (prefabRenderer == null) continue;

            // Szukamy .fbx o takiej samej nazwie
            string[] fbxGuids = AssetDatabase.FindAssets($"{prefabName} t:Model", new[] { modelFolder });
            if (fbxGuids.Length == 0)
            {
                Debug.LogWarning($"⚠️ Nie znaleziono modelu FBX dla: {prefabName}");
                continue;
            }

            string fbxPath = AssetDatabase.GUIDToAssetPath(fbxGuids[0]);
            GameObject fbxModel = AssetDatabase.LoadAssetAtPath<GameObject>(fbxPath);
            if (fbxModel == null) continue;

            MeshRenderer fbxRenderer = fbxModel.GetComponentInChildren<MeshRenderer>();
            if (fbxRenderer == null || fbxRenderer.sharedMaterial == null) continue;

            // Kopiujemy materiał
            Material[] fbxMaterials = fbxRenderer.sharedMaterials;
            prefabRenderer.sharedMaterials = fbxMaterials;

            Debug.Log($"✅ Skopiowano materiał z {fbxModel.name} do {prefab.name}");
            fixedCount++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"🎉 Gotowe! Naprawiono: {fixedCount} prefabów.");
    }
}
