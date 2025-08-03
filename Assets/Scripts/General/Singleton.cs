using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected abstract bool DontDestroy { get; }

    protected virtual void Awake()
    {
        Instance = this as T;
        if(DontDestroy)
            DontDestroyOnLoad(this);
    }

    protected virtual void OnDestroy()
    {
        Instance = null;
    }
}