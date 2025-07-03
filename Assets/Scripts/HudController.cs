using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour
{
    public static HudController Instance;

    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] TMP_Text interactionText;

    public void EnableInteractionText(string text)
    {
        interactionText.text = text + "E";
        interactionText.gameObject.SetActive(true);
    }
    
    public void DisableInteractionText(string text)
    {
        interactionText.gameObject.SetActive(false);
    }
}
