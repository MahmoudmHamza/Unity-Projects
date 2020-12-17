using UnityEngine;

/// <summary>
/// Check if instance already exists.
/// if not, set instance to of type T
/// if instance already exists and it's not this.
/// Then destroy this. This enforces our singleton pattern, 
/// meaning there can only ever be one instance of a given type.
/// //Sets this to not be destroyed when reloading scene.
/// </summary>
/// <typeparam name="T">the typr of singleton</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Check if we want to persist this gameObject between loads
    /// </summary>
    [SerializeField]
    protected bool IsPersistant = false;

    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                CheckForOtherInstances(instance);
            }
            else
            {
                instance = FindObjectOfType<T>();
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        if (IsPersistant)
        {
            CheckForOtherInstances(instance);
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// Checks for another objects of type T and destroy duplicates
    /// </summary>
    /// <param name="instance">current instance</param>
    private static void CheckForOtherInstances(T instance)
    {
        T[] currentInstances = FindObjectsOfType<T>();
        for (int i = 0; i < currentInstances.Length; i++)
        {
            if (instance != null && instance != currentInstances[i])
            {
                Destroy(currentInstances[i].gameObject);
            }
        }
    }
}
