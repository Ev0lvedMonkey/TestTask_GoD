using System;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    AK
}

[Serializable]
public class WeaponModel
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private Sprite _weaponIcon;
    [SerializeField] private int _damage;
    [SerializeField] private ItemType _ammoType; 

    public WeaponType WeaponType => _weaponType;
    public Sprite WeaponIcon => _weaponIcon;
    public int Damage => _damage;
    public ItemType AmmoType => _ammoType;
}
