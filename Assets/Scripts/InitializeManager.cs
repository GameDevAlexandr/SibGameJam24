using UnityEngine;
using UnityEngine.Events;

public class InitializeManager : MonoBehaviour
{
    public UnityEvent Init;
    private void Awake()
    {
        Init.Invoke();
    }
}
