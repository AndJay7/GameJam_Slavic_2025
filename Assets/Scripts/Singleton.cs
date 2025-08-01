using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static Singleton<T> Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    protected virtual void OnDestroy()
    {
        Instance = null;
    }
}