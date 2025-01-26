using UnityEngine;
using Zenject;

public class CharacterView : AliveObjectView
{
    [Inject]
    private void Construct(Character character)
    {
        _aliveObject = character;
    }

}
