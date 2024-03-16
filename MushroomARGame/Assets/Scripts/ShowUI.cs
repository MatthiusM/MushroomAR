using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [SerializeField] private GameObject infoTextMenu;

    private void OnEnable()
    {
        CupGameManager.Instance.AddOnEnter(CupGameStates.End, DisplayUI);
    }

    private void DisplayUI()
    {
        infoTextMenu.SetActive(true);
    }
}
