using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private Sprite _weaponIcon;
    [SerializeField] private int _damage;

    public WeaponType WeaponType => _weaponType;
    public Sprite WeaponIcon => _weaponIcon;
    public int Damage => _damage;
}