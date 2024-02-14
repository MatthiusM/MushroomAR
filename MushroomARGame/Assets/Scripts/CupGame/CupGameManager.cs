using System;
using System.Collections;
using UnityEngine;

public class CupGameManager : MonoBehaviour
{
    public event Action<Action> OnStart;
    public event Action<Action> OnPlayRound;
    public event Action OnPicking;

    private void Start()
    {
        StartCoroutine(ExecuteGameFlow());
    }

    private IEnumerator ExecuteGameFlow()
    {
        yield return WaitForEvent(OnStart);
        yield return WaitForEvent(OnPlayRound);
        OnPicking?.Invoke();
    }

    private IEnumerator WaitForEvent(Action<Action> gameEvent)
    {
        bool isComplete = false;
        gameEvent?.Invoke(() => { isComplete = true; });
        yield return new WaitUntil(() => isComplete);
    }
}
