
using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int CurrentHealth { get; private set; }
    
    public Stats damage;
    public Stats armor;

    void Awake()
    {
        CurrentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            {
            TakeDamage(10);
            }
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, Int32.MaxValue);
        
        CurrentHealth -= damage;
        Debug.Log(transform.name + " taken " + damage + " damage.");
        
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + "died.");
    }
}
