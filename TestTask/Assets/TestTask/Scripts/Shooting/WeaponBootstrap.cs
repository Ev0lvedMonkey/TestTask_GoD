using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WeaponBootstrap : MonoBehaviour
{
    [SerializeField] private List<WeaponView> _weaponButtonsList = new();
    [SerializeField] private Button _shootButton;

    private WeaponViewModel _weaponViewModel;
    private Enemy _enemy;

    private const string WeaponDataFolder = "ScriptableObjects/WeaponsConfig";

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        WeaponDataList weaponDataList = Resources.Load<WeaponDataList>(WeaponDataFolder);
        _weaponViewModel = new(weaponDataList, _enemy);
        foreach (var item in _weaponButtonsList) 
            item.Init(_weaponViewModel);
        _shootButton.onClick.AddListener(_weaponViewModel.Shoot);
    }

    [Inject]
    private void Construct(Enemy enemy)
    {
        _enemy = enemy;
    }
}
