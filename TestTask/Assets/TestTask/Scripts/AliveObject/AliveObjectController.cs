using UnityEngine;

[RequireComponent(typeof(AliveObject))]
public class AliveObjectController : MonoBehaviour
{
    [SerializeField] private AliveObjectView _aliveObjectView;

    private void Awake()
    {
        _aliveObjectView.Init();
    }
}
