using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; 

public class DisableARPlaneOnPrefabPlaced : MonoBehaviour
{
    private ARPlaneManager arPlaneManager;

    private PlacePrefabOnPlane placePrefabOnPlane;

    private void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        placePrefabOnPlane = GetComponent<PlacePrefabOnPlane>();
    }

    private void OnEnable()
    {
        placePrefabOnPlane.onPlacedPrefab += DisableARPlane;
    }

    private void OnDisable()
    {
        placePrefabOnPlane.onPlacedPrefab -= DisableARPlane;
    }

    private void DisableARPlane()
    {
        if (!arPlaneManager.enabled) return;

        arPlaneManager.enabled = false;

        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }

}
