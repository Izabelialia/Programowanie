using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public Image healthBar;

    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (playerStats != null && healthBar != null)
        {
            healthBar.fillAmount = (float)playerStats.currentHp / playerStats.maxHp;
        }
    }
}