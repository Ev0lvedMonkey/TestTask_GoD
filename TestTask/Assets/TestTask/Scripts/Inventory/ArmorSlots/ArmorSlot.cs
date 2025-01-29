using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public abstract class ArmorSlot : Slot, IPointerClickHandler
{
    protected ArmorItem _armorItem;
    protected Character _character;
    private InventoryManager _inventoryManager;
    private Sprite _deafaultSprite;

    public override void Init(int slotId, Canvas canvas = null, PopUpLoader popUpLoader = null)
    {
        CurrentItem.Value = null;
        _armorItem = null;
        _deafaultSprite = _itemImage.sprite;
        base.Init(slotId, canvas, popUpLoader);
    }

    public override void SetItem(Item item, int itemAmount)
    {
        base.SetItem(item, itemAmount);
        EquipArmor((ArmorItem)item);
    }

    public void EquipArmor(ArmorItem armorItem)
    {
        CurrentItem.Value = armorItem;
        _armorItem = armorItem;
        _itemsSlotCount = 1;
        EquipUniqArmor();
        UpdateSlotData();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            Unequip();
    }

    private void Unequip()
    {
        _inventoryManager.GetNewItem(_armorItem);
        CurrentItem.Value = null;
        _armorItem = null;
        UnequipUniqArmor();
        UpdateSlotData();
    }

    protected override void UpdateSlotData()
    {
        _armorItem = (ArmorItem)CurrentItem.Value;
        if (_armorItem == null)
        {
            _itemImage.sprite = _deafaultSprite;
            _itemText.text = "0";
        }
        else
        {
            _itemImage.sprite = _armorItem.Sprite;
            _itemText.text = _armorItem.Defense.ToString();
        }
    }

    protected abstract void UnequipUniqArmor();

    protected virtual void EquipUniqArmor()
    {
        if (_armorItem == null)
            return;
    }


        [Inject]
    private void Construct(Character character, InventoryManager inventoryManager)
    {
        _character = character;
        _inventoryManager = inventoryManager;
    }
}
