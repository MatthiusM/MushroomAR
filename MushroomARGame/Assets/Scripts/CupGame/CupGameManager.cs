using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CupGameManager : MonoBehaviour
{
    public event Action<Action> onStart;
    public event Action<Action> onPlayRound;

    void Start()
    {
        // Example trigger
        StartCoroutine(WaitForEventToComplete());
    }

    public IEnumerator WaitForEventToComplete()
    {
        bool startIsComplete = false;

        onStart?.Invoke(() => { startIsComplete = true; });
        yield return new WaitUntil(() => startIsComplete);

        bool startPlayRound = false;

        onPlayRound?.Invoke(() => { startPlayRound = true; });
        yield return new WaitUntil(() => startPlayRound);
    }
}
