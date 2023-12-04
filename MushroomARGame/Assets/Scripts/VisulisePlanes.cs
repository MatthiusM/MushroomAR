using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class VisulisePlanes : MonoBehaviour
{
    public GameObject spawn_prefab;

    ARRaycastManager arrayman;

    List<ARRaycastHit> hitList = new();

    // Start is called before the first frame update
    void Start()
    {
        arrayman = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(arrayman.Raycast(Input.GetTouch(0).position, hitList, TrackableType.PlaneWithinPolygon))
            {
                var hitpose = hitList[0].pose;
                Instantiate(spawn_prefab, hitpose.position, hitpose.rotation);
            }
        }
    }
}
