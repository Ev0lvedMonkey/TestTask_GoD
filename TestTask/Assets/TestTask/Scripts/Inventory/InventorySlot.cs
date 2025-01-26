using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI References")]
    public Image Background;
    public Image ItemIcon;
    public TMP_Text StackText;
    public Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;

    public Item CurrentItem { get; private set; }
    private int currentStack;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetItem(Item item, int amount)
    {
        CurrentItem = item;
        currentStack = amount;

        ItemIcon.sprite = item.Sprite;
        ItemIcon.gameObject.SetActive(true);

        UpdateStackText();
    }

    public void ClearSlot()
    {
        CurrentItem = null;
        currentStack = 0;

        ItemIcon.sprite = null;
        ItemIcon.gameObject.SetActive(false);
        StackText.text = "";
    }

    public void OnItemDropped(Item item, int amount, Action<Item, int> onExcess)
    {
        if (CurrentItem == null)
        {
            // Пустой слот: добавляем предмет
            SetItem(item, amount);
        }
        else if (CurrentItem.Type == item.Type)
        {
            // Слот занят предметом того же типа: стакуем
            int totalAmount = currentStack + amount;
            if (totalAmount <= CurrentItem.MaxStackAmount)
            {
                SetItem(CurrentItem, totalAmount);
            }
            else
            {
                int excess = totalAmount - CurrentItem.MaxStackAmount;
                SetItem(CurrentItem, CurrentItem.MaxStackAmount);
                onExcess?.Invoke(item, excess);
            }
        }
        else
        {
            // Слот занят другим предметом: меняем местами
            Item tempItem = CurrentItem;
            int tempStack = currentStack;

            SetItem(item, amount);
            onExcess?.Invoke(tempItem, tempStack);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CurrentItem == null) return;

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        originalParent = transform.parent;
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CurrentItem == null) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<InventorySlot>())
        {
            var targetSlot = eventData.pointerEnter.GetComponent<InventorySlot>();
            HandleSlotSwap(targetSlot);
        }
        else
        {
            // Если предмет отпущен вне слота, возвращаем его на место
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    private void HandleSlotSwap(InventorySlot targetSlot)
    {
        if (targetSlot.CurrentItem == null)
        {
            // Перемещение в пустой слот
            targetSlot.SetItem(CurrentItem, currentStack);
            ClearSlot();
        }
        else if (targetSlot.CurrentItem.Type == CurrentItem.Type)
        {
            // Слияние стеков, если типы совпадают
            int maxStackSize = CurrentItem.MaxStackAmount;
            int totalStackSize = targetSlot.currentStack + currentStack;

            if (totalStackSize <= maxStackSize)
            {
                targetSlot.SetItem(CurrentItem, totalStackSize);
                ClearSlot();
            }
            else
            {
                int remainingStack = totalStackSize - maxStackSize;
                targetSlot.SetItem(CurrentItem, maxStackSize);
                SetItem(CurrentItem, remainingStack);
            }
        }
        else
        {
            // Обмен предметов
            var tempItem = targetSlot.CurrentItem;
            var tempStackSize = targetSlot.currentStack;

            targetSlot.SetItem(CurrentItem, currentStack);
            SetItem(tempItem, tempStackSize);
        }

        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    private void UpdateStackText()
    {
        StackText.text = currentStack > 1 ? currentStack.ToString() : "";
    }
}
