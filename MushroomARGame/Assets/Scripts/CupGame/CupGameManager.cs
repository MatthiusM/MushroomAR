using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CupGameManager : MonoBehaviour
{
    public enum GameState
    {
        Starting,
        Playing,
        Picking,
        Ending
    }

    public GameState CurrentState { get; private set; } = GameState.Starting;

    public Action onGameStart;
    public Action<int, float> onPlayRound;
    public Action onPicking;
    public Action onGameEnd;

    private CupGame cupGame;

    private void Awake()
    {
        cupGame = DebugUtility.GetComponentFromGameObject<CupGame>(gameObject);               
    }

    private void Start()
    {
        onGameStart?.Invoke();
    }

    private void OnEnable()
    {
        cupGame.onStartFinished += StartFinished;
    }
    
    private void OnDisable()
    {
        cupGame.onStartFinished -= StartFinished;
    }

    private void StartFinished()
    {
        StartCoroutine(Waitswconds(1f));
        onPlayRound?.Invoke(UnityEngine.Random.Range(6, 9), 0.5f);
    }

    IEnumerator Waitswconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
