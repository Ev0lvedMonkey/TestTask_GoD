using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Character _character;
    [SerializeField] private Enemy _enemy;

    public override void InstallBindings()
    {
        Container.Bind<Character>().FromInstance(_character);
        Container.Bind<Enemy>().FromInstance(_enemy);
    }
}