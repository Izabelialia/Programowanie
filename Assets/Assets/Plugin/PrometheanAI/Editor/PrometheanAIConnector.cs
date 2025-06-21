using UnityEditor;
using UnityEngine;
using PrometheanAI.Modules.TCPServer;

public class PrometheanAIConnector : EditorWindow
{
    private string statusMessage = "Kliknij, aby uruchomiæ PrometheanTcpWrapperClient.";
    private bool isStarted = false;

    [MenuItem("Window/Promethean AI/Connect")]
    public static void ShowWindow()
    {
        GetWindow<PrometheanAIConnector>("Promethean AI");
    }

    void OnGUI()
    {
        GUILayout.Label("Po³¹czenie z Promethean AI", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if (GUILayout.Button("Start TCP Wrapper"))
        {
            StartWrapper();
        }

        GUILayout.Space(10);
        EditorGUILayout.HelpBox(statusMessage, MessageType.Info);
    }

    void StartWrapper()
    {
        try
        {
            new PrometheanTcpWrapperClient(); // tu konstruktor uruchamia ConnectServer
            statusMessage = "PrometheanTcpWrapperClient uruchomiony. Nas³uchuje na 127.0.0.1:1316";
            isStarted = true;
        }
        catch (System.Exception ex)
        {
            statusMessage = "B³¹d: " + ex.Message;
            Debug.LogError(ex);
            isStarted = false;
        }
    }
}

