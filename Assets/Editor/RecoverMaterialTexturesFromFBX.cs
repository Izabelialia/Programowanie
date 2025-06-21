using UnityEngine;
using UnityEditor;
using System.IO;

public class RecoverMaterialTexturesFromFBX
{
    [MenuItem("Tools/URP/Recover BaseMap from FBX")]
    public static void RecoverTextures()
    {
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Resources/" });

        int fixedCount = 0;

        foreach (string guid in prefabGuids)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab == null) continue;

            MeshRenderer renderer = prefab.GetComponentInChildren<MeshRenderer>();
            if (renderer == null || renderer.sharedMaterial == null) continue;

            Material mat = renderer.sharedMaterial;
            if (mat.HasProperty("_BaseMap") && mat.GetTexture("_BaseMap") != null)
                continue; // tekstura już przypisana

            string prefabName = Path.GetFileNameWithoutExtension(prefabPath);
            string[] fbxGuids = AssetDatabase.FindAssets($"{prefabName} t:Model", new[] { "Assets/" });
            if (fbxGuids.Length == 0) continue;

            string fbxPath = AssetDatabase.GUIDToAssetPath(fbxGuids[0]);
            GameObject fbxModel = AssetDatabase.LoadAssetAtPath<GameObject>(fbxPath);
            if (fbxModel == null) continue;

            MeshRenderer fbxRenderer = fbxModel.GetComponentInChildren<MeshRenderer>();
            if (fbxRenderer == null || fbxRenderer.sharedMaterial == null) continue;

            Texture baseTex = fbxRenderer.sharedMaterial.GetTexture("_BaseMap") ??
                              fbxRenderer.sharedMaterial.GetTexture("_MainTex");

            if (baseTex != null)
            {
                mat.SetTexture("_BaseMap", baseTex);
                Debug.Log($"🎨 Przypisano teksturę do: {mat.name}");
                fixedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"✅ Gotowe! Odzyskano tekstury w {fixedCount} materiałach.");
    }
}
