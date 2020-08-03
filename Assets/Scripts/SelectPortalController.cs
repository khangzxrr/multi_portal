using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class SelectPortalController : MonoBehaviour
{
    public PortalController portalController;
    public PlaceObject placeObject;

    private GameObject[] portalPrefabs;

    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        

       // ShowSelectionUI();
    }


    private void SelectVideoForPortal(GameObject portal)
    {
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
        {
            Debug.Log("Video path: " + path);
            if (path != null)
            {
                // Play the selected video
                //Handheld.PlayFullScreenMovie("file://" + path);

                portal.transform.Find("SkyboxSphere").GetComponent<VideoPlayer>().url = "file://" + path;
            }
        }, "Select a video");

        Debug.Log("Permission result: " + permission);
    }

    public void ConfigAndUsePortal(GameObject portal)
    {
        SelectVideoForPortal(portal);
        gameObject.SetActive(false);
        placeObject.PlacingModel(portal);
    }


    public void ShowSelectionUI()
    {
        portalPrefabs = portalController.portals; //get portal from controller
        gameObject.SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {
    }
}
