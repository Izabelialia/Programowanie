using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int health = 100;
    public int armour = 10;
    public int enemyDamage = 10;
    public float damageCooldown = 1f;
    
    private float lastDamageTime = -Mathf.Infinity;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            DestroyEnemy();
    }


    private void OnCollisionEnter(Collision collision)
    {
        PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                playerStats.TakeDamage(this);
                lastDamageTime = Time.time;
                Debug.Log("Gracz dostał obrażenia od przeciwnika!");
            }
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
