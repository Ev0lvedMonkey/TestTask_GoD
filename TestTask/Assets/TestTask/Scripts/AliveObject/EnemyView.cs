using UnityEngine;
using Zenject;

public class EnemyView : AliveObjectView
{
    [Inject]
    private void Construct(Enemy enemy)
    {
        _aliveObject = enemy;
    }
}
