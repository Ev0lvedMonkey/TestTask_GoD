using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> _slotsList;
    [SerializeField] private ItemDataList _itemDataList;

    private void Awake()
    {
        Init();
        SetDefaultItem();
        LogItemTypeAndSlotAmount();
    }

    private void LogItemTypeAndSlotAmount()
    {
        Dictionary<ItemType, int> itemTypeCounts = new();

        foreach (var slot in _slotsList)
        {
            if (slot.CurrentItem.Value == null)
                continue;

            var itemType = slot.CurrentItem.Value.Type;
            var amount = slot.GetSlotAmount();

            if (itemTypeCounts.ContainsKey(itemType))
                itemTypeCounts[itemType] += amount;
            else
                itemTypeCounts[itemType] = amount;
        }

        foreach (var entry in itemTypeCounts)
        {
            Debug.Log($"Item Type: {entry.Key}, Total Count: {entry.Value}");
        }
    }

    public void SetItemToSlot(int slotIndex, ItemType itemType, int amount = -1)
    {
        if (_itemDataList == null || _itemDataList.Items.Count < 0 || _slotsList.Count < 0)
            return;
        Item item = GetItemByType(itemType);
        if (amount == -1)
            _slotsList[slotIndex].SetItem(item, item.MaxStackAmount);
        else
            _slotsList[slotIndex].SetItem(item, amount);
    }

    private void SetDefaultItem()
    {
        if (_itemDataList != null && _itemDataList.Items.Count > 0 && _slotsList.Count > 0)
        {
            SetItemToSlot(0, ItemType.Helemt);
            SetItemToSlot(1, ItemType.Jacket);
            SetItemToSlot(3, ItemType.Pill);
            SetItemToSlot(5, ItemType.PistolBulet);
            SetItemToSlot(6, ItemType.ArmHelmet);
            SetItemToSlot(7, ItemType.ArmJacket);
            SetItemToSlot(11, ItemType.AKBullet);
        }
    }

    private void Init()
    {
        foreach (InventorySlot slot in _slotsList)
            slot.Init();
    }


    private Item GetItemByType(ItemType itemType)
    {
        return _itemDataList.Items.FirstOrDefault(item => item.Type == itemType);
    }
}
