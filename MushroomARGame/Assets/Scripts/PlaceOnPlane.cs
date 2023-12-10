using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

//www.youtube.com. (n.d.). Unity AR Tutorial: AR Raycast - place an object mutiple times. [online] Available at: https://www.youtube.com/watch?v=pC3146FjNC0
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager raycastManager;
    
    [SerializeField]
    private GameObject prefab;

    bool placed = false;

    void Update()
    {
        if (Input.touchCount == 0 || placed)
            return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            List<ARRaycastHit> hits = new();
            if (raycastManager.Raycast(touch.position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                Instantiate(prefab, hitPose.position, hitPose.rotation);
                placed = true;
            }
        }
    }
}

