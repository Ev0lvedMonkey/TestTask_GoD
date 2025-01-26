using System;
using UnityEngine;

public enum ItemType
{
    PistolBulet,
    AKBullet,
    Pill,
    Jacket,
    ArmJacket,
    Helemt,
    ArmHelmet
}

public abstract class Item : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _weight;
    [SerializeField] private int _maxStackAmount;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private ItemType _type;

    public int MaxStackAmount => _maxStackAmount;
    public string Name => _name;
    public Sprite Sprite => _sprite;
    public ItemType Type => _type;
    public float Weight => _weight; 

}