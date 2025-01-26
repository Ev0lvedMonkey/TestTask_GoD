using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "ScriptableObjects/ItemDataList")]
public class ItemDataList : ScriptableObject
{
    
    [SerializeField] private List<Item> _items;

    public IReadOnlyList<Item> Items => _items;

}
