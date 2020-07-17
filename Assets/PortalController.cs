using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{

    public GameObject[] portals; //gameobject with tag "portal"
    void Start()
    {
        
    }

    //moving and resize portal sphere fit with portal
    private void UpdatingPortalSphere()
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
        portals = GameObject.FindGameObjectsWithTag("portal");
        UpdatingPortalSphere();
    }
}
