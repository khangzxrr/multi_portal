using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OnEnterPortal : MonoBehaviour
{
    public Collider portalMask;
    public GameObject portalQuad;
    public Material skyboxSphereMaterial;

    private Boolean switched = false;
    // Start is called before the first frame update
    void Start()
    {
        switched = false;
        skyboxSphereMaterial.SetInt("_StencilComp", (int)CompareFunction.Equal);
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
        return !portalQuad.activeSelf;
    }
    private Boolean isActiveInnerWorld()
    {
        return (skyboxSphereMaterial.GetInt("_StencilComp") == (int)CompareFunction.Disabled) ||
            (skyboxSphereMaterial.GetInt("_StencilComp") == (int)CompareFunction.NotEqual);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other == portalMask)
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
                    portalQuad.SetActive(false);
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
        portalQuad.SetActive(true);
        if (isActiveInnerWorld())
        {
            skyboxSphereMaterial.SetInt("_StencilComp", (int)CompareFunction.NotEqual); //set inside portal
        }
    


        Debug.Log("Exit!");
    }
}
