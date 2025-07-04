using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int bombs = 10;
    public int mushrooms = 10;
    public int coins = 100;
    public int keys = 1;
    
    
    public void AddCoin()
    {
        coins++;
        HudController.Instance.UpdateInventoryUI(this);
    }

    public void UseKey()
    {
        if (keys > 0)
        {
            keys--;
            HudController.Instance.UpdateInventoryUI(this);
        }
    }
}
