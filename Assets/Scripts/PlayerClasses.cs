using UnityEngine;

public class PlayerClasses : MonoBehaviour
{
    public CharacterClass characterClass;
    public LevelSystem levelSystem;

    [Header("Statystyki postaci")]
    public int maxHp;
    public int attack;
    public int defense;
    public float speed;

    void Start()
    {
        ApplyClassStats();
    }

    void Update()
    {
        ApplyClassStats();
    }

    void OnValidate()
    {
        ApplyClassStats();
    }

    void ApplyClassStats()
    {
        int level = 0;

        if (levelSystem != null)
        {
            level = levelSystem.GetLevelNumber();
        }

        switch (characterClass)
        {
            case CharacterClass.Warrior:
                maxHp = 150 + (level * 50);
                attack = 25 + (level * 5);
                defense = 20 + (level * 3);
                speed = 3.5f;
                break;

            case CharacterClass.Archer:
                maxHp = 100 + (level * 30);
                attack = 35 + (level * 7);
                defense = 10 + (level * 2);
                speed = 5f;
                break;

            case CharacterClass.Mage:
                maxHp = 80 + (level * 10);
                attack = 45 + (level * 10);
                defense = 5 + (level * 1);
                speed = 4f;
                break;
        }
    }
}

public enum CharacterClass
{
    Warrior,
    Archer,
    Mage
}

