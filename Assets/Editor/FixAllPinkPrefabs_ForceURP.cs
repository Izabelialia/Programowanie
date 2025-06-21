using UnityEngine;
using UnityEditor;
using System.IO;

public class FixAllPinkPrefabs_ForceURP
{
    [MenuItem("Tools/FORCE Replace All Pink Materials with URP Material")]
    public static void FixNow()
    {
        string resourcesFolder = "Assets/Resources/";
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { resourcesFolder });

        // Stwórz domyślny materiał URP jeśli nie istnieje
        string urpMatPath = "Assets/Resources/_Default_URP_Lit.mat";
        Material urpMat = AssetDatabase.LoadAssetAtPath<Material>(urpMatPath);

        if (urpMat == null)
        {
            urpMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            urpMat.color = Color.gray;
            AssetDatabase.CreateAsset(urpMat, urpMatPath);
            Debug.Log("🎨 Utworzono domyślny materiał URP: _Default_URP_Lit.mat");
        }

        int fixedCount = 0;

        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            MeshRenderer renderer = prefab.GetComponentInChildren<MeshRenderer>();
            if (renderer == null) continue;

            bool needsFix = false;
            Material[] materials = renderer.sharedMaterials;

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] == null || materials[i].shader == null || materials[i].shader.name == "Hidden/InternalErrorShader")
                {
                    materials[i] = urpMat;
                    needsFix = true;
                }
            }

            if (needsFix)
            {
                renderer.sharedMaterials = materials;
                Debug.Log($"✅ Naprawiono: {prefab.name}");
                fixedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"🎉 Gotowe! Naprawiono {fixedCount} prefabów.");
    }
}
