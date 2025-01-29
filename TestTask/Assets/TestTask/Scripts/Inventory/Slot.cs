using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
    public int SlotId;

    [Header("UI References")]
    [SerializeField] protected Image _itemImage;
    [SerializeField] protected TMP_Text _itemText;

    public readonly ReactiveProperty<Item> CurrentItem = new();

    protected int _itemsSlotCount;

    public virtual void Init(int slotId, Canvas canvas = null, PopUpLoader popUpLoader = null)
    {
        SlotId = slotId;
        CurrentItem.Subscribe(_ => UpdateSlotData());
    }

    public virtual void SetItem(Item item, int itemAmount)
    {
        _itemsSlotCount = Mathf.Clamp(itemAmount, 0, item.MaxStackAmount);
        CurrentItem.Value = item;
    }
    public void ClearSlot()
    {
        CurrentItem.Value = null;
        _itemsSlotCount = 0;
        UpdateSlotData();
        Debug.LogWarning($"Slot {gameObject.name} was clear");
    }

    public int GetSlotAmount()
    {
        return _itemsSlotCount;
    }

    protected void EnableSlotComponents()
    {
        _itemImage.sprite = null;
        _itemText.text = "";
    }

    protected abstract void UpdateSlotData();
}
