using UnityEngine;

public static class DebugUtility
{
    public static bool TryGetComponentWithErrorLog<T>(GameObject gameObject, out T component) where T : Component
    {
        if (!gameObject.TryGetComponent<T>(out component))
        {
#if UNITY_EDITOR
            Debug.LogError($"Component of type {typeof(T)} not found on the GameObject named {gameObject.name}.", gameObject);
#endif
            return false;
        }
        return true;
    }
}
