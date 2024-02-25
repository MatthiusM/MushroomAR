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

    void Update()
    {
        // Handle mouse input
        if (Input.GetMouseButtonDown(0) && IsFlickable)
        {
            ProcessInput(Input.mousePosition);
        }

        // Handle touch input
        if (Input.touchCount > 0 && IsFlickable)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) 
            {
                ProcessInput(touch.position);
            }
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
