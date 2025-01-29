using System;
using UnityEngine;

[Serializable]
public class DefaultSlotItem
{
    [Range(1, 30)] public int SlotId;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private bool _isEmptySLot = true;

    public ItemType ItemType => _itemType;
    public bool IsEmptySlot => _isEmptySLot;
}