using R3;
using UnityEngine;
using Zenject;

public class Character : AliveObject
{
    private const int Damage = 15;
    private Enemy _enemy;
    private Armor _headArmor;
    private Armor _torsoArmor;

    protected override void Awake()
    {
        base.Awake();
        _enemy.AliveObjectHealthPoint.Skip(2).Subscribe(_ => TakeDamage(Damage));
    }

    public override void TakeDamage(int damage)
    {
        bool isHeadshot = Random.value > 0.5f; 
        int reducedDamage = CalculateReducedDamage(damage, isHeadshot);

        AliveObjectHealthPoint.Value -= reducedDamage;
        AliveObjectHealthPoint.Value = Mathf.Clamp(AliveObjectHealthPoint.Value, IAlive.MinHealthPoint, IAlive.MaxHealthPoint);

        Debug.Log(isHeadshot
            ? $"Head hit! Reduced damage: {reducedDamage}"
            : $"Torso hit! Reduced damage: {reducedDamage}");
    }

    public void EquipHeadArmor(Armor armor)
    {
        _headArmor = armor;
    }

    public void EquipTorsoArmor(Armor armor)
    {
        _torsoArmor = armor;
    }

    private int CalculateReducedDamage(int damage, bool isHeadshot)
    {
        if (isHeadshot && _headArmor != null)
        {
            return _headArmor.CalculateReducedDamage(damage);
        }
        if (!isHeadshot && _torsoArmor != null)
        {
            return _torsoArmor.CalculateReducedDamage(damage);
        }
        return damage;
    }

    [Inject]
    private void Construct(Enemy enemy)
    {
        _enemy = enemy;
    }
}