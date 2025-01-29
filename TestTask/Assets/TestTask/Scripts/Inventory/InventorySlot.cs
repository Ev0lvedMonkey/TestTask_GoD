using R3;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : Slot, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private const string TempSlotPrefabPath = "Prefabs/TempCell";

    private Canvas _canvas;
    private PopUpLoader _popUpLoader;
    private TempInventorySlot _tempSLot;

    public override void Init(int slotId, Canvas canvas, PopUpLoader popUpLoader)
    {
        SlotId = slotId;
        _canvas = canvas;
        _popUpLoader = popUpLoader;
        CurrentItem.Subscribe(_ => UpdateSlotData());
    }

    public void SetItemAmount(int itemAmount)
    {
        _itemsSlotCount += itemAmount;
        _itemsSlotCount = Mathf.Clamp(_itemsSlotCount, 0, CurrentItem.Value.MaxStackAmount);
        UpdateSlotData();
    }

    public void ReduceStackSize(int amount)
    {
        _itemsSlotCount -= amount;
        if (_itemsSlotCount <= 0)
            ClearSlot();
        else
            _itemText.text = _itemsSlotCount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            _popUpLoader.Load(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"OnBeginDrag {gameObject.name}");
        if (CurrentItem.Value == null && _tempSLot == null)
            return;
        InstantiateTempSlot();
        EnableSlotComponents();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"OnDrag {gameObject.name}");
        if (CurrentItem.Value == null && _tempSLot == null)
            return;
        _tempSLot.RectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"OnEndDrag {gameObject.name}");
        ItemTransfer(eventData);
        DestroyTempSlot();
        UpdateSlotData();
    }

    private void ItemTransfer(PointerEventData eventData)
    {
        InventorySlot hoveredSlot;
        if (eventData.pointerEnter.transform.parent.TryGetComponent(out InventorySlot slot))
        {
            Item slotItem = slot.CurrentItem.Value;
            if (slotItem == null)
            {
                hoveredSlot = slot;
                hoveredSlot.SetItem(CurrentItem.Value, _itemsSlotCount);
                hoveredSlot.UpdateSlotData();
                ClearSlot();
                return;
            }
            else
            {
                Item tempItem;
                int tempSlotAmount;
                if (slotItem.Type != CurrentItem.Value.Type)
                {
                    tempItem = slotItem;
                    tempSlotAmount = slot.GetSlotAmount();
                    slot.SetItem(CurrentItem.Value, _itemsSlotCount);
                    SetItem(tempItem, tempSlotAmount);
                    return;
                }
                else
                {
                    int slotItemAmount = slot.GetSlotAmount();
                    int neededAmount = CurrentItem.Value.MaxStackAmount - slotItemAmount;
                    bool canItTransfer = _itemsSlotCount >= neededAmount;
                    if (canItTransfer)
                    {
                        ReduceStackSize(neededAmount);
                        slot.SetItemAmount(neededAmount);
                    }
                    else
                    {
                        slot.SetItemAmount(_itemsSlotCount);
                        ReduceStackSize(_itemsSlotCount);
                    }
                }
            }
        }
        else return;
    }

    private void InstantiateTempSlot()
    {
        TempInventorySlot tempSLot = Resources.Load<TempInventorySlot>(TempSlotPrefabPath);
        _tempSLot = Instantiate(tempSLot, transform.position, Quaternion.identity, _canvas.transform);
        _tempSLot.SetItem(CurrentItem.Value);
    }

    private void DestroyTempSlot()
    {
        if (_tempSLot == null)
            return;
        Destroy(_tempSLot.gameObject);
        _tempSLot = null;
    }

    protected override void UpdateSlotData()
    {
        _itemImage.sprite = CurrentItem.Value?.Sprite;
        _itemText.text = _itemsSlotCount > 1 ? _itemsSlotCount.ToString() : "";
    }
}
