using System;
using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Camera arCamera;
    private LayerMask layerMask;

    private bool IsFlickable = false;

    private CupGameManager cupGameManager;

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
    }

    private void OnDisable()
    {
        cupGameManager.OnPicking -= EnableFlick;
    }

    private void EnableFlick()
    {
        IsFlickable = true;
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
                flickableCup?.Flick();
            }
        }
    }
}
