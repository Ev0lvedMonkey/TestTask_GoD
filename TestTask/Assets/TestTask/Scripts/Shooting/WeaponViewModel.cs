using R3;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    AK
}

public class WeaponViewModel
{
    public readonly ReactiveProperty<WeaponData> SelectedWeapon = new();
    public readonly Dictionary<WeaponType, WeaponData> WeaponDictionary;

    private readonly Enemy _enemy;

    public WeaponViewModel(string weaponDataFolder, Enemy enemy)
    {
        _enemy = enemy;
        WeaponDictionary = new Dictionary<WeaponType, WeaponData>
        {
            { WeaponType.Pistol, Resources.Load<WeaponData>($"{weaponDataFolder}/PistolData") },
            { WeaponType.AK, Resources.Load<WeaponData>($"{weaponDataFolder}/AKData") }
        };

        SelectWeapon(WeaponType.Pistol);
    }

    public void SelectWeapon(WeaponType weaponType)
    {
        if (WeaponDictionary.TryGetValue(weaponType, out var weapon))
        {
            SelectedWeapon.Value = weapon;
            Debug.Log($"Выбрано оружие: {SelectedWeapon.Value.WeaponType}");
        }
    }

    public void Shoot()
    {
        if (SelectedWeapon != null)
        {
            _enemy.TakeDamage(SelectedWeapon.Value.Damage);
            Debug.Log($"Выстрел из {SelectedWeapon.Value.WeaponType}. Урон: {SelectedWeapon.Value.Damage}");
        }
    }

}