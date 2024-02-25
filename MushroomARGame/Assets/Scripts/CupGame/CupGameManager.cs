using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CupGameStates
{
    Start,
    PlayRound,
    Picking,
    End,
}

public class CupGameManager : GameManager<CupGameStates>
{
    private void Start()
    {
        Instance.SwitchState(CupGameStates.Start);
    }
}
