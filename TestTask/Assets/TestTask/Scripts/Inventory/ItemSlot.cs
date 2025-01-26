using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI stackSizeText;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;

    public Item CurrentItem { get; private set; }
    public int CurrentStackSize { get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void AddItem(Item item, int stackSize)
    {
        CurrentItem = item;
        CurrentStackSize = stackSize;

        itemIcon.sprite = item.Sprite;
        itemIcon.enabled = true;

        if (stackSize > 1)
        {
            stackSizeText.text = stackSize.ToString();
            stackSizeText.enabled = true;
        }
        else
        {
            stackSizeText.enabled = false;
        }
    }

    public void ClearSlot()
    {
        CurrentItem = null;
        CurrentStackSize = 0;

        itemIcon.sprite = null;
        itemIcon.enabled = false;
        stackSizeText.enabled = false;
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

        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<ItemSlot>())
        {
            var targetSlot = eventData.pointerEnter.GetComponent<ItemSlot>();
            HandleSlotSwap(targetSlot);
        }
        else
        {
            // Если предмет отпущен вне слота, возвращаем его на место
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    private void HandleSlotSwap(ItemSlot targetSlot)
    {
        if (targetSlot.CurrentItem == null)
        {
            // Перемещение в пустой слот
            targetSlot.AddItem(CurrentItem, CurrentStackSize);
            ClearSlot();
        }
        else if (targetSlot.CurrentItem.Type == CurrentItem.Type)
        {
            // Слияние стеков, если типы совпадают
            int maxStackSize = CurrentItem.MaxStackAmount;
            int totalStackSize = targetSlot.CurrentStackSize + CurrentStackSize;

            if (totalStackSize <= maxStackSize)
            {
                targetSlot.AddItem(CurrentItem, totalStackSize);
                ClearSlot();
            }
            else
            {
                int remainingStack = totalStackSize - maxStackSize;
                targetSlot.AddItem(CurrentItem, maxStackSize);
                AddItem(CurrentItem, remainingStack);
            }
        }
        else
        {
            // Обмен предметов
            var tempItem = targetSlot.CurrentItem;
            var tempStackSize = targetSlot.CurrentStackSize;

            targetSlot.AddItem(CurrentItem, CurrentStackSize);
            AddItem(tempItem, tempStackSize);
        }

        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}
