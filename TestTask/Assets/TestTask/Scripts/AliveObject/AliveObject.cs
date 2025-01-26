using R3;
using UnityEngine;

public abstract class AliveObject : MonoBehaviour, IAlive
{
    public readonly ReactiveProperty<int> AliveObjectHealthPoint = new();
    public ReadOnlyReactiveProperty<bool> AliveObjectDie;

    protected DisposableBag _disposables = new();
    private Armor _armor;

    public virtual void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }

    public virtual void TakeDamage(int damage)
    {
        int reducedDamage = _armor != null ? _armor.CalculateReducedDamage(damage) : damage;
        AliveObjectHealthPoint.Value -= reducedDamage;
        AliveObjectHealthPoint.Value = Mathf.Clamp(AliveObjectHealthPoint.Value, IAlive.MinHealthPoint, IAlive.MaxHealthPoint);
        Debug.Log($"{gameObject.name} получил {reducedDamage} урона. Осталось здоровья: {AliveObjectHealthPoint.Value}");
    }

    public void EquipArmor(Armor armor)
    {
        _armor = armor;
    }

    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} bro died");
    }

    protected void Init()
    {
        AliveObjectHealthPoint.Value = IAlive.MaxHealthPoint;
        AliveObjectDie = AliveObjectHealthPoint.Select(amount => amount <= 0).ToReadOnlyReactiveProperty();
        AliveObjectDie.Where(isDead => isDead).Subscribe(_ => Die()).AddTo(ref _disposables);
    }
}
