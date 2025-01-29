using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Character _character;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameOverUI _gameOverUI;
    [SerializeField] private HelmetArmorSlot _helmetArmorSlot;
    [SerializeField] private TorsoArmorSlot _torsoArmorSlot;



    public override void InstallBindings()
    {
        Container.Bind<Character>().FromInstance(_character);
        Container.Bind<Enemy>().FromInstance(_enemy);
        Container.Bind<InventoryManager>().FromInstance(_inventoryManager);
        Container.Bind<Canvas>().FromInstance(_canvas);
        Container.Bind<GameOverUI>().FromInstance(_gameOverUI);
        Container.Bind<HelmetArmorSlot>().FromInstance(_helmetArmorSlot);
        Container.Bind<TorsoArmorSlot>().FromInstance(_torsoArmorSlot);

    }
}