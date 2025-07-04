
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Statystyki gracza")]
    public int maxHp = 100;
    public int currentHp;

    public int armour = 10;
    public int damage = 25;
    
    [Header("Leczenie")]
    public int healAmount = 25;
    public Inventory inventory;

    void Start()
    {
        currentHp = maxHp;
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
        int reducedDamage = incomingDamage - armour;

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
        if (inventory != null && inventory.mushrooms > 0 && currentHp < maxHp)
        {
            inventory.mushrooms--;
            currentHp += healAmount;

            if (currentHp > maxHp)
                currentHp = maxHp;

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
}
