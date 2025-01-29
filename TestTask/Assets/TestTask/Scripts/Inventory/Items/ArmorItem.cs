using UnityEngine;

public abstract class ArmorItem : Item
{
    [SerializeField] private int _defense;

    public int Defense => _defense;
}