using UnityEngine;

public static class DebugUtility
{
    // Tries to get a component of type T from a GameObject and logs an error if not found, now returns the component or null.
    public static T GetComponentFromGameObject<T>(GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
        {
#if UNITY_EDITOR
            Debug.LogError($"Component of type {typeof(T)} not found on the GameObject named {gameObject.name}.", gameObject);
#endif
        }
        return component;
    }

    // Gets a component of type T from a GameObject with the specified name. Logs an error if the GameObject is not found.
    public static T GetComponentFromName<T>(string gameObjectName) where T : Component
    {
        GameObject targetGameObject = GameObject.Find(gameObjectName);
        if (targetGameObject != null)
        {
            return targetGameObject.GetComponent<T>();
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError($"GameObject named {gameObjectName} not found.");
#endif
            return null;
        }
    }

    // Gets a component of type T from a GameObject with the specified tag. Logs an error if the GameObject is not found.
    public static T GetComponentFromTag<T>(string tag) where T : Component
    {
        GameObject targetGameObject = GameObject.FindWithTag(tag);
        if (targetGameObject != null)
        {
            return targetGameObject.GetComponent<T>();
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError($"GameObject with tag {tag} not found.");
#endif
            return null;
        }
    }

    // Finds the first instance of a component of type T in the scene. This method searches through all GameObjects in the scene, which is inefficient.
    public static T GetComponentFromScene<T>() where T : Component
    {
        T component = GameObject.FindObjectOfType<T>();
#if UNITY_EDITOR
        if (component == null)
        {
            Debug.LogError($"Component of type {typeof(T).Name} not found in the scene.");
        }
#endif
        return component;
    }
}
