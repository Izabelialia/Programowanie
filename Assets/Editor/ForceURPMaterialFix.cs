using UnityEngine;
using UnityEditor;

public class ForceURPMaterialFix : MonoBehaviour
{
    [MenuItem("Tools/URP/FORCE Replace All Standard Shaders with URP")]
    public static void ForceUpgrade()
    {
        string[] matGuids = AssetDatabase.FindAssets("t:Material");

        int fixedCount = 0;
        foreach (string guid in matGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat == null || mat.shader == null)
                continue;

            // Jeśli używa Standard shadera
            if (mat.shader.name == "Standard")
            {
                // Zmień na URP/Lit
                mat.shader = Shader.Find("Universal Render Pipeline/Lit");
                Debug.Log($"✅ Przypisano URP shader do: {mat.name}");
                fixedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"🎉 Gotowe! Przekonwertowano {fixedCount} materiałów na URP.");
    }
}
