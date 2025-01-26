using UnityEngine;

[CreateAssetMenu(fileName = "NewHelmetItem", menuName = "Inventory/Helmet Item")]
public class HelmetItem : Item
{
    [SerializeField] private int _defense;

    public int Defense => _defense;
}