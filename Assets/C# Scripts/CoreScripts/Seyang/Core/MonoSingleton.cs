using Unity.VisualScripting;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected virtual bool IsDontDestroyOnLoad => true;
    
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null || instance.IsUnityNull())
                instance = FindObjectOfType<T>();
            if (instance == null || instance.IsUnityNull())
                instance = Instantiate(new GameObject(typeof(T).Name)).AddComponent<T>();
            return instance;
        }
        private set => instance = value;
    }
    public virtual void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        if(IsDontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}