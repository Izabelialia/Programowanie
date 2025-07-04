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
    
    [Header("Equipment UI")]
    [SerializeField] TMP_Text bombCountText;
    [SerializeField] TMP_Text mushroomCountText;

    public void EnableInteractionText(string text)
    {
        interactionText.text = text + "E";
        interactionText.gameObject.SetActive(true);
    }
    
    public void DisableInteractionText(string text)
    {
        interactionText.gameObject.SetActive(false);
    }
    
    public void UpdateInventoryUI(Inventory inventory)
    {
        bombCountText.text = ": " + inventory.bombs;
        mushroomCountText.text = ": " + inventory.mushrooms;
    }
}
