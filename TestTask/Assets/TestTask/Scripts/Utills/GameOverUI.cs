using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void Enable()=>
        gameObject.SetActive(true);
    public void Disable()=>
        gameObject.SetActive(false);
}
