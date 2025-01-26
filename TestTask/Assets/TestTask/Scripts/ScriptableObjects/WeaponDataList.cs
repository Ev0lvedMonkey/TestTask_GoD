using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataList", menuName = "ScriptableObjects/WeaponDataList")]
public class WeaponDataList : ScriptableObject
{
    [SerializeField] private List<WeaponModel> _weapons;

    public IReadOnlyList<WeaponModel> Weapons => _weapons;
}
