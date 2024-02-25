using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager<T> : MonoBehaviour where T : Enum
{
    private static GameManager<T> instance;
    public static GameManager<T> Instance
    {
        get { return instance; }
    }

    private T currentState;
    public T CurrentState
    {
        get { return currentState; }
        private set { currentState = value; }
    }

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

    public Action GetOnEnterAction(T state)
    {
        return onEnterState[state];
    }

    public Action GetOnExitAction(T state)
    {
        return onExitState[state];
    }
}
