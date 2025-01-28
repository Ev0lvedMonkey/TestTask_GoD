using R3;
using UnityEngine;

public abstract class AliveObject : MonoBehaviour, IAlive
{
    public readonly ReactiveProperty<int> AliveObjectHealthPoint = new();
    public ReadOnlyReactiveProperty<bool> AliveObjectDie;

    protected DisposableBag _disposables = new();

    protected virtual void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }

    public virtual void TakeDamage(int damage)
    {
        AliveObjectHealthPoint.Value -= damage;
        AliveObjectHealthPoint.Value = Mathf.Clamp(AliveObjectHealthPoint.Value, IAlive.MinHealthPoint, IAlive.MaxHealthPoint);
    }

    public void TakeHeal(int healthPoint)
    {
        AliveObjectHealthPoint.Value += healthPoint;
        AliveObjectHealthPoint.Value = Mathf.Clamp(AliveObjectHealthPoint.Value, IAlive.MinHealthPoint, IAlive.MaxHealthPoint);
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
        //AliveObjectHealthPoint.Subscribe(_ =>
        //Debug.Log($"{gameObject.name} changed HP. Now: {AliveObjectHealthPoint.Value} HP"));
    }
}


