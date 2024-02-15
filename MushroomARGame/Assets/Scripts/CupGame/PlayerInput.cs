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
    public bool FinishedFlick
    {
        get { return finishedFlick; }
        private set { finishedFlick = value; }
    }

    private CupGameManager cupGameManager;

    public Action OnFinishedFlick;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Cups");
        GameObject arSessionOrigin = GameObject.Find("AR Session Origin");
        if (arSessionOrigin != null)
        {
            arCamera = arSessionOrigin.GetComponentInChildren<Camera>();
        }

        cupGameManager = DebugUtility.GetComponentFromName<CupGameManager>("CupGame");
    }

    private void OnEnable()
    {
        cupGameManager.OnPicking += EnableFlick;
        OnFinishedFlick += DisableClick;
    }

    private void OnDisable()
    {
        cupGameManager.OnPicking -= EnableFlick;
        OnFinishedFlick -= DisableClick;
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsFlickable)
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                IFlickable flickableCup = hit.transform.GetComponent<IFlickable>();
                flickableCup?.Flick(OnFinishedFlick);
            }
        }
    }
}
