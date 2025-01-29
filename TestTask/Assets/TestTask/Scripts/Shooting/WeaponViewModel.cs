using R3;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponViewModel
{
    public readonly ReactiveProperty<WeaponModel> SelectedWeapon = new();
    public readonly Dictionary<WeaponType, WeaponModel> WeaponDictionary;

    private readonly Enemy _enemy;
    private readonly InventoryManager _inventoryManager;
    private readonly Dictionary<WeaponType, int> AmmunitionConsumptionByShot = new() {
        {WeaponType.Pistol, 1},
        {WeaponType.AK, 3},
    };
    public WeaponViewModel(WeaponDataList weaponDataList, InventoryManager inventoryManager, Enemy enemy)
    {
        _enemy = enemy;
        _inventoryManager = inventoryManager;
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
        if (SelectedWeapon.Value == null) return;

        InventorySlot ammoSlot = (InventorySlot)_inventoryManager.FindAmmoSlot(SelectedWeapon.Value.AmmoType);
        if (ammoSlot == null || ammoSlot.GetSlotAmount() <= 0)
        {
            Debug.LogWarning("No ammo available!");
            return;
        }

        _enemy.TakeDamage(SelectedWeapon.Value.Damage);
        Debug.Log($"{SelectedWeapon.Value.WeaponType} shot. Damage: {SelectedWeapon.Value.Damage}");

        ammoSlot.ReduceStackSize(AmmunitionConsumptionByShot[SelectedWeapon.Value.WeaponType]);
    }
}
