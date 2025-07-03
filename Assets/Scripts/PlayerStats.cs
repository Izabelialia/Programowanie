
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Statystyki gracza")]
    public int maxHp = 100;
    public int currentHp;

    public int armour = 10;
    public int damage = 25;
    public float atackSpeed = 1;

    void Start()
    {
        currentHp = maxHp;
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

    public void Die()
    {
        Debug.Log("Gracz zginął.");
    }
}
