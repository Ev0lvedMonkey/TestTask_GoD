using UnityEngine;

public class TempInputHandler : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Enemy _enemy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _character.TakeDamage(10);
        if (Input.GetKeyDown(KeyCode.D))
            _enemy.TakeDamage(10);
    }
}
