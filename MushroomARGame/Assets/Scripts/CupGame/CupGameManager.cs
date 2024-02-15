using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CupGameManager : MonoBehaviour
{
    public event Action<Action> OnStartWithCallback;
    public event Action OnStart;

    public event Action<Action> OnPlayRoundWithCallback;
    public event Action OnPlayRound;

    public event Action<Action> OnPickingWithCallback;
    public event Action OnPicking;

    private PlayerInput playerInput;

    public GameObject I;

    private void Start()
    {
        playerInput = DebugUtility.GetComponentFromName<PlayerInput>("PlayerInput");
        StartCoroutine(GameFlow());
    }

    private IEnumerator GameFlow()
    {
        yield return StartCoroutine(WaitForEvent(OnStart, OnStartWithCallback));
        yield return StartCoroutine(WaitForEvent(OnPlayRound, OnPlayRoundWithCallback));
        yield return StartCoroutine(WaitForEvent(OnPicking, OnPickingWithCallback));
        yield return new WaitUntil(() => playerInput.FinishedFlick);

        I.SetActive(true);

        // wait for event here?
    }
    private IEnumerator WaitForEvent(Action simpleEvent, Action<Action> actionEvent)
    {
        if (simpleEvent != null)
        {
            Coroutine withoutAction = StartCoroutine(WaitForEventSimple(simpleEvent));
            yield return withoutAction;
        }

        if (actionEvent != null)
        {
            Coroutine withAction = StartCoroutine(WaitForEvent(actionEvent));
            yield return withAction;
        }
    }

    private IEnumerator WaitForEvent(Action<Action> gameEvent)
    {
        bool isComplete = false;
        gameEvent?.Invoke(() => { isComplete = true; });
        yield return new WaitUntil(() => isComplete);
    }

    private IEnumerator WaitForEventSimple(Action gameEvent)
    {
        bool isInvoked = false;
        if (gameEvent != null)
        {
            gameEvent.Invoke();
            isInvoked = true;
        }
        yield return new WaitUntil(() => isInvoked);
    }
}
