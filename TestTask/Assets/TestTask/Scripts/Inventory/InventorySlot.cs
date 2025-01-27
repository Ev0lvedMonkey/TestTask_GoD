using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int SlotId;

    [Header("UI References")]
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _stackText;

    public readonly ReactiveProperty<Item> CurrentItem = new();

    private int _itemsSlotCount;

    public void Init(int slotId)
    {
        SlotId = slotId;
        CurrentItem.Subscribe(_ => UpdateSlotData());
    }

    public void SetItem(Item item, int itemAmount)
    {
        _itemsSlotCount = itemAmount;
        CurrentItem.Value = item;
    }

    public int GetSlotAmount()
    {
        return _itemsSlotCount;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
    private void UpdateSlotData()
    {
        _itemImage.sprite = CurrentItem.Value?.Sprite;
        _stackText.text = _itemsSlotCount > 1 ? _itemsSlotCount.ToString() : "";
    }
}
