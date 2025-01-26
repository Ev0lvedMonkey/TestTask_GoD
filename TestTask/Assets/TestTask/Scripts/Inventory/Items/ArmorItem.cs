using UnityEngine;

[CreateAssetMenu(fileName = "NewArmorItem", menuName = "Inventory/Armor Item")]
public class ArmorItem : Item
{
    [SerializeField] private int _defense;

    public int Defense => _defense;
}