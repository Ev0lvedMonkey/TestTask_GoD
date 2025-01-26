using UnityEngine;

public class Armor
{
    public int Defense { get; private set; }

    public Armor(int defense)
    {
        Defense = Mathf.Max(0, defense);
    }

    public int CalculateReducedDamage(int damage)
    {
        return Mathf.Max(0, damage - Defense);
    }
}