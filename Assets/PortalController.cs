using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{

    public GameObject[] portals; //gameobject with tag "portal"
    public List<GameObject> currentWorkingPortals = new List<GameObject>();
    void Start()
    {
        UpdatePortalsList();
        UpdatingPortalSphereFitWithPortalModel();
    }

    public void UpdatePortalsList()
    {
        portals = GameObject.FindGameObjectsWithTag("portal");
    }
    public void SetPortalsStateExcept(GameObject portal, bool state)
    {
        foreach(GameObject currentPortal in portals)
        {
            if (currentPortal != portal)
            {
                currentPortal.SetActive(state);
            }
        }
    }
    //moving and resize portal sphere fit with portal
    public void UpdatingPortalSphereFitWithPortalModel()
    {
        foreach(GameObject portal in portals)
        {
            var portalQuad = portal.transform.Find("PortalQuad");
            var skyboxSphere = portal.transform.Find("SkyboxSphere");

            skyboxSphere.transform.position = portalQuad.transform.position;
            var meshBoundsRadius = (portalQuad.localScale.x > portalQuad.localScale.y ? portalQuad.localScale.x : portalQuad.localScale.y) * 1.3f;
            skyboxSphere.transform.localScale = new Vector3(meshBoundsRadius, meshBoundsRadius, meshBoundsRadius);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
