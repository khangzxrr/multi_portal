using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObject : MonoBehaviour
{
    private SelectPortalController selectedPortalController;
    public GameObject SelectionPortalCanvas;
    private ARRaycastManager arRaycastManager;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        selectedPortalController = SelectionPortalCanvas.GetComponent<SelectPortalController>();


    }


    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void RotateFaceToCamera(GameObject obj)
    {
        Vector3 targetPostition = new Vector3(Camera.main.transform.position.x,
                                        obj.transform.position.y,
                                        Camera.main.transform.position.z);
        obj.transform.LookAt(targetPostition);

        Vector3 rotateFaceToCamera = new Vector3(0, obj.transform.rotation.eulerAngles.y + 90, 0);
        obj.transform.localRotation = Quaternion.Euler(rotateFaceToCamera);
    }

    void Update()
    {
        if (Input.touchCount > 0 && !SelectionPortalCanvas.activeSelf && !IsPointerOverUIObject())
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
                if (arRaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    GameObject newPortal = Instantiate(selectedPortalController.selectedPortal); //clone new portal
                    newPortal.transform.position = hitPose.position;
                    RotateFaceToCamera(newPortal);

                    




                }
            }
        }
    }
}
