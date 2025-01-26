using R3;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [Header("Weapon Type")]
    [SerializeField] private WeaponType _weaponType;

    [Header("UI Elements")]
    [SerializeField] private Image _pistolBackground;
    [SerializeField] private Button _pistolButton;
    [SerializeField] private Image _pistolIcon;

    private WeaponViewModel _viewModel;
    private Sprite _weaponSprite;

    private readonly Color UnselectedColor = Color.gray;
    private readonly Color SelectedColor = Color.green;

    public void Init(WeaponViewModel viewModel)
    {
        _viewModel = viewModel;
        _weaponSprite = _viewModel.WeaponDictionary[_weaponType].WeaponIcon;
        InitializeUI();
        BindViewModel();
    }

    private void InitializeUI()
    {
        _pistolIcon.sprite = _weaponSprite;
        _pistolButton.onClick.AddListener(() => _viewModel.SelectWeapon(_weaponType));
    }

    private void BindViewModel()
    {
        _viewModel.SelectedWeapon.Subscribe(UpdateUI);
    }

    private void UpdateUI(WeaponData weapon)
    {
        _pistolBackground.color = weapon.WeaponType == _weaponType ? SelectedColor : UnselectedColor;
        Debug.Log($"Выбран {weapon.WeaponType}");
    }
}