using UnityEngine;

public class PopUpLoader 
{
    private PopUp _popUp;
    private InventoryManager _inventoryManager;
    private HelmetArmorSlot _helmetArmorSlot;
    private TorsoArmorSlot _torsoArmorSlot;
    private Character _character;
    private Canvas _canvas;
    private const string TempSlotPrefabPath = "Prefabs/ItemPopUp";

    public PopUpLoader(Canvas canvas, Character character,
        InventoryManager inventoryManager, TorsoArmorSlot torsoArmorSlot, HelmetArmorSlot helmetArmorSlot)
    {
        _character = character;
        _canvas = canvas;
        _inventoryManager = inventoryManager;
        _helmetArmorSlot = helmetArmorSlot;
        _torsoArmorSlot = torsoArmorSlot;
    }

    public void Load(InventorySlot slot)
    {
        PopUp popUp = Resources.Load<PopUp>(TempSlotPrefabPath);
        if (popUp == null)
        {
            Debug.LogError($"Prefab not found at path: {TempSlotPrefabPath}");
            return;
        }

        _popUp = Object.Instantiate(popUp, _canvas.transform);
        if (_popUp == null)
        {
            Debug.LogError("PopUp component not found on instantiated prefab.");
            return;
        }

        _popUp.Open(slot, _character, _inventoryManager, _torsoArmorSlot, _helmetArmorSlot);
    }

}
