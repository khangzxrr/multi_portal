using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OnEnterPortal : MonoBehaviour
{
    public GameObject currentPortalQuad;


    public GameObject[] portals;
    public Material skyboxSphereMaterial;

    private Boolean switched = false;
    // Start is called before the first frame update
    void Start()
    {
        switched = false;
        skyboxSphereMaterial.SetInt("_StencilComp", (int)CompareFunction.Equal);
    }

    public bool isPortalMask(Collider other)
    {
        foreach(GameObject portal in portals)
        {
            Collider currentPortalCollider = portal.transform.Find("PortalQuad").gameObject.GetComponent<Collider>();
            if (currentPortalCollider == other)
            {
                Debug.Log("YEAH1");
                currentPortalQuad = portal.transform.Find("PortalQuad").gameObject;
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }


    private Boolean IsDeactivatedPortalQuad()
    {
        return !currentPortalQuad.activeSelf;
    }
    private Boolean isActiveInnerWorld()
    {
        return (skyboxSphereMaterial.GetInt("_StencilComp") == (int)CompareFunction.Disabled) ||
            (skyboxSphereMaterial.GetInt("_StencilComp") == (int)CompareFunction.NotEqual);
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("trigged! " + other.gameObject.name);
        if (isPortalMask(other))
        {

            var distance = Vector3.Distance(this.transform.position, other.transform.position);
            if (distance < 0.25f && !switched)
            {

                if (!isActiveInnerWorld())
                {
                    skyboxSphereMaterial.SetInt("_StencilComp", (int)CompareFunction.Disabled); //set inside portal
                }
                else
                {
                    currentPortalQuad.SetActive(false);
                    skyboxSphereMaterial.SetInt("_StencilComp", (int)CompareFunction.Equal); //outside portal
                    
                }
                
                switched = true;
                Debug.Log("Switched NOTEQUAL!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        

        switched = false;
        currentPortalQuad.SetActive(true);
        if (isActiveInnerWorld())
        {
            skyboxSphereMaterial.SetInt("_StencilComp", (int)CompareFunction.NotEqual); //set inside portal
        }
    


        Debug.Log("Exit!");
    }
}
