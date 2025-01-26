using UnityEngine;

[CreateAssetMenu(fileName = "NewPillItem", menuName = "Inventory/Pill Item")]
public class PillItem : Item
{
    [SerializeField] private int _healthRegenerationAmount;

    public int HealthRegenerationAmount => _healthRegenerationAmount;
}