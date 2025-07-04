
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Statystyki gracza")]
    public int currentHp;
    
    [Header("Leczenie")]
    public int healAmount = 25;
    public Inventory inventory;
    
    private PlayerClasses playerClasses;

    void Start()
    {
        playerClasses = GetComponent<PlayerClasses>();
        
        if (playerClasses != null)
        {
            currentHp = playerClasses.maxHp;
        }
        HudController.Instance.UpdateInventoryUI(inventory);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            HealWithMushroom();
        }
    }

    public void TakeDamage(Enemy enemy)
    {
        int incomingDamage = enemy.enemyDamage;
        int reducedDamage = incomingDamage - GetDefense();

        if (reducedDamage < 0)
            reducedDamage = 0;
        else
        {
            currentHp -= reducedDamage;
        }
        
        if (currentHp <= 0)
        {
            Die();
        }
    }
    
    public void HealWithMushroom()
    {
        if (inventory != null && inventory.mushrooms > 0 && currentHp < GetMaxHp())
        {
            inventory.mushrooms--;
            currentHp += healAmount;

            if (currentHp > GetMaxHp())
                currentHp = GetMaxHp();
            
            HudController.Instance.UpdateInventoryUI(inventory);

            Debug.Log("Gracz uleczony o " + healAmount + " HP.");
        }
        else
        {
            Debug.Log("Brak grzybków lub pełne zdrowie.");
        }
    }

    public void Die()
    {
        Debug.Log("Gracz zginął.");
    }
    private int GetMaxHp()
    {
        return playerClasses != null ? playerClasses.maxHp : 100;
    }

    private int GetDefense()
    {
        return playerClasses != null ? playerClasses.defense : 10;
    }
}
