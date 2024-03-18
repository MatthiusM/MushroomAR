using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [SerializeField] private GameObject infoTextMenu;

    public void DisplayUI()
    {
        infoTextMenu.SetActive(true);
    }
}
