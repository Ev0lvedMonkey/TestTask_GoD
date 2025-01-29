using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultInventoryConfig", menuName = "ScriptableObjects/DefaultInventoryConfig")]
public class DefaultInventoryConfig : ScriptableObject
{
    [SerializeField] private List<DefaultSlotItem> _defaultSlots;

    public IReadOnlyList<DefaultSlotItem> DefaultSlots => _defaultSlots;

    private void OnValidate()
    {
        for (int i = 0; i < _defaultSlots.Count; i++)
        {
            if (_defaultSlots[i].SlotId == 0)
                _defaultSlots[i].SlotId = i + 1; 
            _defaultSlots[i].SlotId = Mathf.Clamp(_defaultSlots[i].SlotId, 1, 30); 
        }
    }
}