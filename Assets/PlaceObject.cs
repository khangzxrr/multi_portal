using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObject : MonoBehaviour
{
    public SelectPortalController selectedPortalController;
    private ARRaycastManager arRaycastManager;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
                if (arRaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    selectedPortalController.selectedPortal.transform.position = hitPose.position;
                    selectedPortalController.selectedPortal.transform.rotation = hitPose.rotation;
                }
            }
        }
    }
}
