using UnityEngine;

public class PopUpLoader 
{
    private PopUp _popUp;
    private Canvas _canvas;
    private const string TempSlotPrefabPath = "Prefabs/ItemPopUp";

    public PopUpLoader(Canvas canvas)
    {
        _canvas = canvas;
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

        _popUp.Open(slot);
    }

}
