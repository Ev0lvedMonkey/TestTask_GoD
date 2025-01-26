using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> Slots;
    public ItemDataList Database;
    public GameObject DraggedItemIcon;

    private Item draggedItem;
    private int draggedAmount;
    private InventorySlot originalSlot;

    void Start()
    {
        if (Database != null && Database.Items.Count > 0 && Slots.Count > 0)
        {
            Slots[0].SetItem(Database.Items[0], 1);
            Slots[2].SetItem(Database.Items[0], 1);
        }
    }

    void Update()
    {
        if (draggedItem != null)
        {
            DraggedItemIcon.transform.position = Input.mousePosition;
        }
    }

    public void StartDragging(Item item, int amount, InventorySlot origin)
    {
        draggedItem = item;
        draggedAmount = amount;
        originalSlot = origin;

        DraggedItemIcon.GetComponent<Image>().sprite = item.Sprite;
        DraggedItemIcon.SetActive(true);
        origin.ClearSlot();
    }

    public void StopDragging()
    {
        if (draggedItem == null) return;

        // Проверяем, наведён ли курсор на слот
        InventorySlot slotUnderCursor = GetSlotUnderCursor();
        if (slotUnderCursor != null)
        {
            slotUnderCursor.OnItemDropped(draggedItem, draggedAmount, HandleExcessItems);
        }
        else
        {
            // Возвращаем предмет в исходный слот, если не наведено на слот
            originalSlot.SetItem(draggedItem, draggedAmount);
        }

        draggedItem = null;
        draggedAmount = 0;
        DraggedItemIcon.SetActive(false);
    }

    private InventorySlot GetSlotUnderCursor()
    {
        foreach (var slot in Slots)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                slot.GetComponent<RectTransform>(), Input.mousePosition))
            {
                return slot;
            }
        }
        return null;
    }

    private void HandleExcessItems(Item item, int excess)
    {
        foreach (var slot in Slots)
        {
            if (slot.CurrentItem == null)
            {
                slot.SetItem(item, excess);
                return;
            }
        }

        // Если нет свободных слотов, избыточные предметы уничтожаются
        Debug.Log($"Excess items of {item.Name} were destroyed: {excess}");
    }
}
