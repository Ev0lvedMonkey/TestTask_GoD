using Zenject;

public class Enemy : AliveObject
{
    private InventoryManager _inventoryManager;
    [Inject]
    private void Construct(InventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
    }

    public override void Die()
    {
        base.Die();
        _inventoryManager.GetRandomNewItem();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if(AliveObjectHealthPoint.Value <= 0)
            TakeHeal(IAlive.MaxHealthPoint);
    }

}
