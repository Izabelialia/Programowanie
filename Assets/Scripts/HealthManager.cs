using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerClasses playerClasses;
    public Image healthBar;

    void Start()
    {
        if (playerStats == null)
            playerStats = GetComponent<PlayerStats>();

        if (playerClasses == null)
            playerClasses = GetComponent<PlayerClasses>();
    }
    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (playerStats != null && healthBar != null)
        {
            healthBar.fillAmount = (float)playerStats.currentHp / playerClasses.maxHp;
        }
    }
}