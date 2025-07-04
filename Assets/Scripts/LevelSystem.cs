using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public int level = 0;
    private int experience = 0;
    public int experienceToNextLevel = 100;

    public PlayerClasses playerClasses;

    private void Start()
    {
        if (playerClasses == null)
            playerClasses = GetComponent<PlayerClasses>();
    }

    public void AddExperience(int amount)
    {
        experience += amount;

        while (experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }
}
