using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Character _character;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private Canvas _canvas;

    public override void InstallBindings()
    {
        Container.Bind<Character>().FromInstance(_character);
        Container.Bind<Enemy>().FromInstance(_enemy);
        Container.Bind<InventoryManager>().FromInstance(_inventoryManager);
        Container.Bind<Canvas>().FromInstance(_canvas);

    }
}