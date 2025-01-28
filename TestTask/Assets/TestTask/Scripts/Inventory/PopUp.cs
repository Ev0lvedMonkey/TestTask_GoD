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
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _weightText;

    public readonly ReactiveProperty<Item> CurrentItem = new();
    private InventorySlot _slot;


    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        CurrentItem.Subscribe(_ => UpdateSlotData());
        _removeItemButton.onClick.AddListener(() => _slot.ClearSlot());
        _closePopUpButton.onClick.AddListener(Close);
    }

    public void Open(InventorySlot slot)
    {
        _slot = slot;
        CurrentItem.Value = slot.CurrentItem.Value;
    }

    public void Close()
    {

    }

    private void UpdateSlotData()
    {
        _itemImage.sprite = CurrentItem.Value.Sprite;
        _itemNameText.text = CurrentItem.Value.Name;
        _weightText.text = CurrentItem.Value.Weight.ToString();
    }
}
