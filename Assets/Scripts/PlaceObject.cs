using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObject : MonoBehaviour
{
    public PortalController portalController;

    private ARRaycastManager arRaycastManager;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();


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

    public void PlacingModel(GameObject selectedPortal)
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Vector2 screenCenter = new Vector2(x, y);

        List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
        if (arRaycastManager.Raycast(screenCenter, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = s_Hits[0].pose;

            GameObject newPortal = Instantiate(selectedPortal); //clone new portal
            portalController.currentWorkingPortals.Add(newPortal); //add cloned portal to working portal

            newPortal.transform.position = hitPose.position;

            newPortal.transform.localScale = new Vector3(1f, 1.03f, 0.7f); //rescale back to normal!

            RotateFaceToCamera(newPortal);
            newPortal.transform.Find("InformationUI").GetComponent<Floating>().enabled = true; //enable floating effect




        }
    }

    void Update()
    {
    }
}
