using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectPortalController : MonoBehaviour
{

    public GameObject[] portalPrefabs;
    public GameObject selectionPortalPlane;
    public GameObject selectedPortal { get; set; }
    public Camera selectionModelCamera;
    public Camera mainCamera;

    private int currentSelectedPortalIndex = 0; 
    // Start is called before the first frame update
    void Start()
    {
        ShowSelectionUI();
    }

    public bool IsSelectingModel()
    {
        if (selectionModelCamera.enabled)
        {
            return true;
        }
        return false;
    }

    public void EndSelectingModel()
    {
        selectionModelCamera.enabled = false;
        mainCamera.enabled = true;
    }
    public void ShowSelectionUI()
    {
        selectionModelCamera.enabled = true;
        Camera.main.enabled = false;

        for (int i = 0; i < portalPrefabs.Length; i++)
        {
            Vector3 displayModelPosition = new Vector3(selectionPortalPlane.transform.position.x,
                                                       selectionPortalPlane.transform.position.y + 0.08f,
                                                       selectionPortalPlane.transform.position.z);
            portalPrefabs[i] = (GameObject)Instantiate(portalPrefabs[i], displayModelPosition, Quaternion.identity);
            portalPrefabs[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        HideAllModels();
        selectedPortal = portalPrefabs[currentSelectedPortalIndex];
        selectedPortal.SetActive(true);
    }

    public void HideAllModels()
    {
        foreach(GameObject model in portalPrefabs)
        {
            model.SetActive(false);
        }
    }

    public void UpdateSelectionPortal()
    {
        HideAllModels();
        selectedPortal = portalPrefabs[currentSelectedPortalIndex];
        selectedPortal.SetActive(true);
    }
    public void NextModel()
    {
        currentSelectedPortalIndex++;
        if (currentSelectedPortalIndex == portalPrefabs.Length)
        {
            currentSelectedPortalIndex = 0;
        }

        UpdateSelectionPortal();
    }

    public void BackModel()
    {
        currentSelectedPortalIndex--;
        if (currentSelectedPortalIndex == -1)
        {
            currentSelectedPortalIndex = portalPrefabs.Length - 1;
        }

        UpdateSelectionPortal();
    }

    void ShowUp()
    {
        
        //portalPrefabs[0].transform.position = selectionPortalPlane.transform.position;

    }


    // Update is called once per frame
    void Update()
    {
        selectedPortal.transform.Rotate(0, 50.0f * Time.deltaTime, 0);
        Debug.Log(Input.touchCount);
        if (((Input.touchCount > 0) || Input.GetMouseButtonDown(0))
            && (IsSelectingModel())) 
        {
            EndSelectingModel();
        }
    }
}
