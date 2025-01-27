using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI References")]
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _stackText;

    public readonly ReactiveProperty<Item> CurrentItem = new();

    private DisposableBag _disposables = new();
    private int _itemsSlotCount;

    private void OnDestroy()
    {
        _disposables.Dispose();
    }

    public void Init()
    {
        CurrentItem.Subscribe(_ => UpdateSlotData()).AddTo(ref _disposables);
    }

    public void SetItem(Item item, int itemAmount)
    {
        _itemsSlotCount = itemAmount;
        CurrentItem.Value = item;
    }

    public void ClearSlot()
    {

    }

    public void OnItemDropped(Item item, int amount, Action<Item, int> onExcess)
    {

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

    public int GetSlotAmount()
    {
        return _itemsSlotCount;
    }

    private void HandleSlotSwap(InventorySlot targetSlot)
    {

    }

    private void UpdateSlotData()
    {
        _itemImage.sprite = CurrentItem.Value.Sprite;
        _stackText.text = _itemsSlotCount > 1 ? _itemsSlotCount.ToString() : "";
    }
}
