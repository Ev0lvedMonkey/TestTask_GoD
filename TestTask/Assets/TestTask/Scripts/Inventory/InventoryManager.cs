using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> _slotsList;
    [SerializeField] private ItemDataList _itemDataList;

    private const string SaveFileName = "inventory.json";
    private string SaveFilePath => Path.Combine(Application.persistentDataPath, SaveFileName);

    private PopUpLoader _popUpLoader;
    private Canvas _canvas;

    private void Awake()
    {
        Init();
        if (File.Exists(SaveFilePath))
            LoadInventory();
        else
            SetDefaultItem();
        //LogItemTypeAndSlotAmount();

    }

    private void OnApplicationQuit()
    {
        SaveInventory();
    }

    public void GetNewItem()
    {
        InventorySlot slot = _slotsList.FirstOrDefault(slot => slot.CurrentItem.Value == null);
        Item randomItem = _itemDataList.Items[Random.Range(0, _itemDataList.Items.Count - 1)];
        int randomItemAmount = Random.Range(0, randomItem.MaxStackAmount + 1);
        slot.SetItem(randomItem, randomItemAmount);
    }

    public InventorySlot FindAmmoSlot(ItemType ammoType)
    {
        foreach (var slot in _slotsList)
        {
            if (slot.CurrentItem.Value == null)
                continue;
            if (slot.CurrentItem.Value.Type != ammoType)
                continue;
            return slot;
        }
        return null;
    }

    public void SaveInventory()
    {
        List<SlotData> slotDataList = new();

        foreach (var slot in _slotsList)
        {
            if (slot.CurrentItem.Value != null)
            {
                SlotData slotData = new()
                {
                    SlotId = slot.SlotId,
                    ItemType = slot.CurrentItem.Value.Type,
                    Amount = slot.GetSlotAmount()
                };
                slotDataList.Add(slotData);
            }
        }

        string json =
            JsonConvert.SerializeObject(new InventoryData { Slots = slotDataList },
            Newtonsoft.Json.Formatting.Indented);

        File.WriteAllText(SaveFilePath, json);
        Debug.Log("Inventory saved successfully!");
    }

    private void LoadInventory()
    {
        if (!File.Exists(SaveFilePath))
        {
            Debug.LogWarning("Save file not found. Initializing empty inventory.");
            return;
        }

        string json = File.ReadAllText(SaveFilePath);
        InventoryData inventoryData = JsonConvert.DeserializeObject<InventoryData>(json);

        foreach (var slotData in inventoryData.Slots)
        {
            var slot = _slotsList.Find(s => s.SlotId == slotData.SlotId);
            if (slot != null)
            {
                var item = GetItemByType(slotData.ItemType);
                slot.SetItem(item, slotData.Amount);
            }
        }

        Debug.Log("Inventory loaded successfully!");
    }

    public void SetItemToSlot(int slotIndex, ItemType itemType, int amount = -1)
    {
        if (_itemDataList == null || _itemDataList.Items.Count < 0 || _slotsList.Count < 0)
            return;
        Item item = GetItemByType(itemType);
        if (amount == -1)
            _slotsList[slotIndex].SetItem(item, item.MaxStackAmount);
        else
            _slotsList[slotIndex].SetItem(item, amount);
    }

    private void SetDefaultItem()
    {
        if (_itemDataList != null && _itemDataList.Items.Count > 0 && _slotsList.Count > 0)
        {
            SetItemToSlot(0, ItemType.Helemt);
            SetItemToSlot(1, ItemType.Jacket);
            SetItemToSlot(3, ItemType.Pill);
            SetItemToSlot(5, ItemType.PistolBulet);
            SetItemToSlot(6, ItemType.ArmHelmet);
            SetItemToSlot(7, ItemType.ArmJacket);
            SetItemToSlot(11, ItemType.AKBullet);
            SetItemToSlot(12, ItemType.AKBullet);
        }
    }

    private void Init()
    {
        _popUpLoader = new(_canvas);
        for (int i = 0; i < _slotsList.Count; i++)
        {
            InventorySlot slot = _slotsList[i];
            slot.Init(i, _canvas, _popUpLoader);
        }
    }

    private void LogItemTypeAndSlotAmount()
    {
        Dictionary<ItemType, int> itemTypeCounts = new();

        foreach (var slot in _slotsList)
        {
            if (slot.CurrentItem.Value == null)
                continue;

            var itemType = slot.CurrentItem.Value.Type;
            var amount = slot.GetSlotAmount();

            if (itemTypeCounts.ContainsKey(itemType))
                itemTypeCounts[itemType] += amount;
            else
                itemTypeCounts[itemType] = amount;
        }

        foreach (var entry in itemTypeCounts)
        {
            Debug.Log($"Item Type: {entry.Key}, Total Count: {entry.Value}");
        }
    }

    private Item GetItemByType(ItemType itemType)
    {
        return _itemDataList.Items.FirstOrDefault(item => item.Type == itemType);
    }

    [Inject]
    private void Construct(Canvas canvas)
    {
        _canvas = canvas;
        Debug.Log($"Inject sucsess");
    }
}
