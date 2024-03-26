using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager<T> : MonoBehaviour where T : Enum
{
    private static GameManager<T> instance;
    public static GameManager<T> Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager<T>>();

                if (instance == null)
                {
                    GameObject gameManagerObject = new(typeof(T).Name + "Manager");
                    instance = gameManagerObject.AddComponent(typeof(GameManager<T>)) as GameManager<T>;
                }
            }

            return instance;
        }
    }

    private T currentState;
    public T CurrentState => currentState;

    protected Dictionary<T, Action> onEnterState = new();
    protected Dictionary<T, Action> onExitState = new();

    protected GameManager()
    {
        Array states = Enum.GetValues(typeof(T));
        foreach (T state in states)
        {
            onEnterState[state] = null; 
            onExitState[state] = null;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void SwitchState(T newState)
    {
        onExitState[currentState]?.Invoke();

        currentState = newState;

        onEnterState[newState]?.Invoke();
    }

    private void UpdateStateAction(Dictionary<T, Action> dictionary, T state, Action callback, bool add)
    {
        if (add)
        {
            dictionary[state] += callback;
        }
        else
        {
            dictionary[state] -= callback;
        }
    }

    public void AddOnEnter(T state, Action callback) => UpdateStateAction(onEnterState, state, callback, add: true);
    public void RemoveOnEnter(T state, Action callback) => UpdateStateAction(onEnterState, state, callback, add: false);

    public void AddOnExit(T state, Action callback) => UpdateStateAction(onExitState, state, callback, add: true);
    public void RemoveOnExit(T state, Action callback) => UpdateStateAction(onExitState, state, callback, add: false);

}
