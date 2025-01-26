using R3;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponViewModel
{
    public readonly ReactiveProperty<WeaponModel> SelectedWeapon = new();
    public readonly Dictionary<WeaponType, WeaponModel> WeaponDictionary;

    private readonly Enemy _enemy;

    public WeaponViewModel(WeaponDataList weaponDataList, Enemy enemy)
    {
        _enemy = enemy;

        WeaponDictionary = weaponDataList.Weapons.ToDictionary(weapon => weapon.WeaponType);

        SelectWeapon(WeaponType.Pistol); 
    }

    public void SelectWeapon(WeaponType weaponType)
    {
        if (WeaponDictionary.TryGetValue(weaponType, out var weapon))
        {
            SelectedWeapon.Value = weapon;
        }
    }

    public void Shoot()
    {
        if (SelectedWeapon.Value != null)
        {
            _enemy.TakeDamage(SelectedWeapon.Value.Damage);
            Debug.Log($"{SelectedWeapon.Value.WeaponType} shooted. Damage: {SelectedWeapon.Value.Damage}");
        }
    }
}
