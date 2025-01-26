using UnityEngine;

public class TempInputHandler : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Enemy _enemy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _character.TakeHeal(50);
    }
}
