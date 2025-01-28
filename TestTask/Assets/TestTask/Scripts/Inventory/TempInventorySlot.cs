using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TempInventorySlot : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Item _draggingItem;

    public RectTransform RectTransform;

    private readonly Vector2 CellSize = new(100, 90);

    public Item DraggingItem => _draggingItem;

    private void OnValidate()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        _draggingItem = null;
    }

    public void SetItem(Item item)
    {
        _draggingItem = item;
        RectTransform.sizeDelta = CellSize;
        _itemImage.sprite = _draggingItem.Sprite;
    }
}
