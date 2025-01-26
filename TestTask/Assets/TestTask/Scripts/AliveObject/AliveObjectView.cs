using TMPro;
using UnityEngine;
using UnityEngine.UI;
using R3;

public abstract class AliveObjectView : MonoBehaviour
{
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private TextMeshProUGUI _healthAmountText;

    protected AliveObject _aliveObject;

    public void Init()
    {
        _aliveObject.AliveObjectHealthPoint.Subscribe(UpdateUI);
    }

    private void UpdateUI(int healthAmount)
    {
        float fillAmount = Mathf.Clamp01((float)healthAmount / IAlive.MaxHealthPoint);
        _healthBarFill.fillAmount = fillAmount;
        _healthAmountText.text = healthAmount.ToString();
    }
}
