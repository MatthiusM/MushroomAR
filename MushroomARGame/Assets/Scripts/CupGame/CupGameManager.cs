using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CupGameManager : MonoBehaviour
{
    private CupGame game;

    private SwapCups swap;

    private void Start()
    {
        game = GetComponent<CupGame>();
        swap = GetComponent<SwapCups>();

        game.finishedSetup.AddListener(EnableSwap);
    }

    public void EnableSwap()
    {
        game.enabled = false; 
        swap.enabled = true;
    }

}
