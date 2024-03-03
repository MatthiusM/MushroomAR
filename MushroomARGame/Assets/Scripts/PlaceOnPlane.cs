using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;

//www.youtube.com. (n.d.). Unity AR Tutorial: AR Raycast - place an object mutiple times. [online] Available at: https://www.youtube.com/watch?v=pC3146FjNC0
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager raycastManager;
    
    [SerializeField]
    private GameObject prefab;

    bool placed = false;

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
        if (placed)
            return;

        List<ARRaycastHit> hits = new();
        if (raycastManager.Raycast(inputPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            Instantiate(prefab, hitPose.position, hitPose.rotation);

            placed = true;
        }
    }

}

