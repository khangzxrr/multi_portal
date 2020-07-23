using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraEnterPortal : MonoBehaviour
{
    private GameObject currentPortalQuad; //will be setted when Colliders touch each others
    private GameObject currentPortalskyboxSphere; //will be setted when Colliders touch each others
    private GameObject currentPortalModel;

    private Boolean switched = false;
    // Start is called before the first frame update
    void Start()
    {
        switched = false;
        //GetSkyboxMaterial().SetInt("_StencilComp", (int)CompareFunction.Equal);
        //default is Equal
    }

    private Material GetSkyboxMaterial()
    {
        return currentPortalskyboxSphere.GetComponent<Renderer>().material;
    }

    public bool isPortalMask(Collider other)
    {
        GameObject[] portals = GameObject.Find("PortalController").GetComponent<PortalController>().portals;
        foreach (GameObject portal in portals)
        {
            Collider currentPortalCollider = portal.transform.Find("PortalQuad").gameObject.GetComponent<Collider>();
            if (currentPortalCollider == other)
            {
                //update current portal information
                currentPortalQuad = portal.transform.Find("PortalQuad").gameObject;
                currentPortalskyboxSphere = portal.transform.Find("SkyboxSphere").gameObject;
                currentPortalModel = portal;

                return true;
            }
        }
        return false;
    }

    private void SetSkyboxSphereSize(float size)
    {
        currentPortalskyboxSphere.transform.localScale = new Vector3(size, size, size);
    }

    // Update is called once per frame
    void Update()
    {
        //skyboxSphere.transform.position = Camera.main.transform.position; //always follow player
       

    }


    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void SwitchRenderCurrentPortalQuad(bool visible)
    {
        currentPortalQuad.GetComponent<MeshRenderer>().enabled = visible;
    }


    private Boolean IsDeactivatedPortalQuad()
    {
        return !currentPortalQuad.activeSelf;
    }
    private Boolean isActiveInnerWorld()
    {
        return (GetSkyboxMaterial().GetInt("_StencilComp") == (int)CompareFunction.Disabled) ||
            (GetSkyboxMaterial().GetInt("_StencilComp") == (int)CompareFunction.NotEqual);
    }
    private void OnTriggerStay(Collider other)
    {
        if (isPortalMask(other))
        {

            var distance = Vector3.Distance(this.transform.position, other.transform.position);
            if (distance < 0.25f && !switched)
            {

                if (!isActiveInnerWorld())
                {
                    GetSkyboxMaterial().SetInt("_StencilComp", (int)CompareFunction.Disabled); //set inside portal
                    SetSkyboxSphereSize(10);

                    GameObject.Find("PortalController").GetComponent<PortalController>().SetPortalsStateExcept(currentPortalModel, false);
                    //disable all other portals
                }
                else
                {
                    SwitchRenderCurrentPortalQuad(false);
                    GetSkyboxMaterial().SetInt("_StencilComp", (int)CompareFunction.Equal); //outside portal

                    GameObject.Find("PortalController").GetComponent<PortalController>().SetPortalsStateExcept(currentPortalModel, enabled);
                    //disable all other portals

                    GameObject.Find("PortalController").GetComponent<PortalController>().UpdatingPortalSphereFitWithPortalModel();
                    //recalculate sphere when going outside portal

                    

                }

                switched = true;
                Debug.Log("Switched NOTEQUAL!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        

        switched = false;
        SwitchRenderCurrentPortalQuad(true);

        if (isActiveInnerWorld())
        {
            GetSkyboxMaterial().SetInt("_StencilComp", (int)CompareFunction.NotEqual); //set inside portal
        }
    


        Debug.Log("Exit!");
    }
}
