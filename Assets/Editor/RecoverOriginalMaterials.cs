using UnityEngine;
using UnityEditor;
using System.IO;

public class RecoverOriginalMaterials
{
    [MenuItem("Tools/Recover Original Materials (Match by Name)")]
    public static void Recover()
    {
        string root = "Assets/Resources/";
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { root });

        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            MeshRenderer renderer = prefab.GetComponentInChildren<MeshRenderer>();
            if (renderer == null) continue;

            Material[] newMaterials = renderer.sharedMaterials;
            bool updated = false;

            for (int i = 0; i < newMaterials.Length; i++)
            {
                var mat = newMaterials[i];
                if (mat == null || mat.shader == null || mat.shader.name == "Hidden/InternalErrorShader")
                {
                    string matName = prefab.name;
                    string[] candidates = AssetDatabase.FindAssets($"t:Material {matName}");
                    if (candidates.Length > 0)
                    {
                        string newMatPath = AssetDatabase.GUIDToAssetPath(candidates[0]);
                        Material found = AssetDatabase.LoadAssetAtPath<Material>(newMatPath);
                        if (found != null)
                        {
                            newMaterials[i] = found;
                            updated = true;
                            Debug.Log($"✅ Przypisano oryginalny materiał do: {prefab.name}");
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"⚠️ Nie znaleziono materiału dla: {prefab.name}");
                    }
                }
            }

            if (updated)
            {
                renderer.sharedMaterials = newMaterials;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("🎉 Skanowanie zakończone.");
    }
}
