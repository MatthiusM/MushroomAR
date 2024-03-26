using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlacePrefabOnPlane : MonoBehaviour
{
    private ARRaycastManager raycastManager;

    [SerializeField]
    private GameObject prefab;

    public Action onPlacedPrefab;

    private void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnTapEvent += PlaceMiniGame;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnTapEvent -= PlaceMiniGame;
    }

    private void PlaceMiniGame(Vector2 inputPosition)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (!raycastManager.Raycast(inputPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            return;
        }

        Pose hitPose = hits[0].pose;
        Instantiate(prefab, hitPose.position, hitPose.rotation);

        onPlacedPrefab?.Invoke();
        InputManager.Instance.OnTapEvent -= PlaceMiniGame;
    }

}
