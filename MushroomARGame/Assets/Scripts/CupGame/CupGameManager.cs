using System;
using System.Collections;
using UnityEngine;

public class CupGameManager : MonoBehaviour
{
    public Action onGameStart;
    public Action onPlayRound;
    public Action onGameEnd;

    private void Start()
    {        
        onGameStart?.Invoke();
    }
}
