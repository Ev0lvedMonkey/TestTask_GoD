using R3;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _itemImage;
    [SerializeField] private Button _closePopUpButton;
    [SerializeField] private Button _removeItemButton;
    [SerializeField] private Button _uniqButton;
    [SerializeField] private TMP_Text _uniqButtonText;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _weightText;
    [SerializeField] private GameObject _armorView;
    [SerializeField] private TMP_Text _armorText;

    private InventoryManager _inventoryManager;
    private InventorySlot _slot;
    private HelmetArmorSlot _helmetArmorSlot;
    private TorsoArmorSlot _torsoArmorSlot;
    private Item _currentItem;
    private Character _character;
    private Action _uniqAction;

    private Dictionary<Type, Action> UniqActionDictionary;
    private Dictionary<Type, string> UniqTextDictionary;

    public void Open(InventorySlot slot, Character character,
        InventoryManager inventoryManager, TorsoArmorSlot torsoArmorSlot, HelmetArmorSlot helmetArmorSlot)
    {
        _slot = slot;
        _inventoryManager = inventoryManager;
        _character = character;
        _helmetArmorSlot = helmetArmorSlot;
        _torsoArmorSlot = torsoArmorSlot;
        _currentItem = slot.CurrentItem.Value;
        InitComponents();
        UpdateSlotData();
    }

    private void Close()
    {
        Destroy(this.gameObject);
    }

    private void InitComponents()
    {
        InitDictionaries();
        Type itemType = _currentItem.GetType();
        if ((_currentItem as ArmorItem) != null)
        {
            ShowArmorView();
        }
        if (UniqActionDictionary.ContainsKey(itemType)
            && UniqTextDictionary.ContainsKey(itemType))
        {
            _uniqAction = UniqActionDictionary[itemType];
            _uniqButtonText.text = UniqTextDictionary[itemType];
        }
        _uniqButton.onClick.AddListener(() => UniqAction());
        _removeItemButton.onClick.AddListener(() => { _slot.ClearSlot(); Close(); });
        _closePopUpButton.onClick.AddListener(Close);
    }

    private void InitDictionaries()
    {
        UniqActionDictionary = new()
        {
            {typeof(PillItem),() => {_character.TakeHeal(50); _slot.ReduceStackSize(1);}},
            {typeof(BulletItem), () => {_inventoryManager.GetNewItem(_currentItem);}},
            {typeof(TorsoArmorItem), () => {_torsoArmorSlot.EquipArmor((ArmorItem)_currentItem); _slot.ClearSlot(); }},
            {typeof(HeadArmorItem), () => {_helmetArmorSlot.EquipArmor((ArmorItem)_currentItem);_slot.ClearSlot(); }},

        };
        UniqTextDictionary = new()
        {
            {typeof(PillItem), "Ћечить"},
            {typeof(BulletItem), " упить"},
            {typeof(TorsoArmorItem), "Ёкипировать"},
            {typeof(HeadArmorItem), "Ёкипировать"},
        };

    }

    private void UniqAction()
    {
        _uniqAction?.Invoke();
        Close();
    }

    private void ShowArmorView()
    {
        _armorView.SetActive(true);
        _armorText.text = (_currentItem as ArmorItem).Defense.ToString();
    }

    private void UpdateSlotData()
    {
        _itemImage.sprite = _currentItem.Sprite;
        _itemNameText.text = _currentItem.Name;
        _weightText.text = _currentItem.Weight.ToString();
    }

}
