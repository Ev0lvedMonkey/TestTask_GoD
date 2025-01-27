using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [Header("Weapon Type")]
    [SerializeField] private WeaponType _weaponType;

    [Header("UI Elements")]
    [SerializeField] private Image _weaponBackground;
    [SerializeField] private Button _weaponButton;
    [SerializeField] private Image _weaponIcon;
    [SerializeField] private TextMeshProUGUI _weaponDamageText;

    private WeaponViewModel _viewModel;
    private WeaponModel _weaponModel;

    private readonly Color UnselectedColor = Color.gray;
    private readonly Color SelectedColor = Color.green;

    public void Init(WeaponViewModel viewModel)
    {
        _viewModel = viewModel;
        _weaponModel  = _viewModel.WeaponDictionary[_weaponType];
        InitializeUI();
        BindViewModel();
    }

    private void InitializeUI()
    {
        _weaponIcon.sprite = _weaponModel.WeaponIcon;
        _weaponDamageText.text = _weaponModel.Damage.ToString();
        _weaponButton.onClick.AddListener(() => _viewModel.SelectWeapon(_weaponType));
    }

    private void BindViewModel()
    {
        _viewModel.SelectedWeapon.Subscribe(UpdateUI);
    }

    private void UpdateUI(WeaponModel weapon)
    {
        _weaponBackground.color = weapon.WeaponType == _weaponType ? SelectedColor : UnselectedColor;
    }
}