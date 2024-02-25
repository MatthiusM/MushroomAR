using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Camera arCamera;
    private LayerMask layerMask;

    private bool IsFlickable = false;

    private bool finishedFlick = false;
    public bool FinishedFlick => finishedFlick;

    public Action OnFinishedFlick;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Cups");
        GameObject arSessionOrigin = GameObject.Find("AR Session Origin");
        if (arSessionOrigin != null)
        {
            arCamera = arSessionOrigin.GetComponentInChildren<Camera>();
        }
    }

    private void OnEnable()
    {
        CupGameManager.Instance.AddOnEnter(CupGameStates.Picking, EnableFlick);
        OnFinishedFlick += DisableClick;
        InputManager.Instance.OnTapEvent += HandleTap;
    }

    private void OnDisable()
    {
        CupGameManager.Instance.AddOnExit(CupGameStates.Picking, EnableFlick);
        OnFinishedFlick -= DisableClick;
        InputManager.Instance.OnTapEvent -= HandleTap;
    }
    private void HandleTap(Vector2 position)
    {
        if (IsFlickable)
        {
            ProcessInput(position);
        }
    }

    private void DisableClick()
    {
        IsFlickable = false;
        finishedFlick = true;
    }

    private void EnableFlick()
    {
        IsFlickable = true;
        finishedFlick = false;
    }

    private void ProcessInput(Vector2 inputPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(inputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            IFlickable flickableCup = hit.transform.GetComponent<IFlickable>();
            flickableCup?.Flick(OnFinishedFlick);
        }
    }

}
