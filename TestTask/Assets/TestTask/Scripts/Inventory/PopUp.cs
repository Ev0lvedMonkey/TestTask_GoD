using R3;
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
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _weightText;

    private InventorySlot _slot;
    private Item _currentItem;

    public void Open(InventorySlot slot)
    {
        _slot = slot;
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
        //todo в зависимоти от типа будет то или иное дейсиве использоваться
        //словарь или что либо такое, надо думать
        //_uniqButton.onClick.AddListener();
        _removeItemButton.onClick.AddListener(() => _slot.ClearSlot());
        _closePopUpButton.onClick.AddListener(Close);
    }
    private void UpdateSlotData()
    {
        _itemImage.sprite = _currentItem.Sprite;
        _itemNameText.text = _currentItem.Name;
        _weightText.text = _currentItem.Weight.ToString();
    }
}
