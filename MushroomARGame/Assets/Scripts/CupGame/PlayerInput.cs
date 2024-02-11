using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Camera arCamera;
    private LayerMask layerMask;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Cups");
        GameObject arSessionOrigin = GameObject.Find("AR Session Origin");
        if (arSessionOrigin != null)
        {
            arCamera = arSessionOrigin.GetComponentInChildren<Camera>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                FlickCup flickCup = hit.transform.GetComponent<FlickCup>();
                if (flickCup != null && (FlickCup.CurrentActiveCup == null))
                {
                    flickCup.onFlickAction?.Invoke();
                }
            }
        }
    }
}
