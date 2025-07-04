using UnityEngine;

public class PlayerClasses : MonoBehaviour
{
    public CharacterClass characterClass;

    [Header("Statystyki postaci")]
    public int maxHp;
    public int attack;
    public int defense;
    public float speed;

    void Start()
    {
        ApplyClassStats();
    }

    void OnValidate()
    {
        ApplyClassStats();
    }

    void ApplyClassStats()
    {
        switch (characterClass)
        {
            case CharacterClass.Warrior:
                maxHp = 150;
                attack = 25;
                defense = 20;
                speed = 3.5f;
                break;

            case CharacterClass.Archer:
                maxHp = 100;
                attack = 35;
                defense = 10;
                speed = 5f;
                break;

            case CharacterClass.Mage:
                maxHp = 80;
                attack = 45;
                defense = 5;
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