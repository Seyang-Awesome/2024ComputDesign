using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    protected virtual bool IsDontDestroyOnLoad => true;
    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<T>();
            if(IsDontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this) Destroy(gameObject);
    }
}