using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextEditor : MonoBehaviour
{
    private TextMeshProUGUI currentField;
    private string currentText;

    public void Start()
    {
        Hide();
    }
    private void Show()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }



    public void SettingTextFor(TextMeshProUGUI textMesh)
    {
        Show();
        transform.Find("InputField").GetComponent<InputField>().text = textMesh.text;
        currentField = textMesh;
    }

    public void OnTextFieldChanged(string text)
    {
        currentText = text;
    }

    public void SaveText()
    {
        currentField.text = currentText;
        Hide();
    }
}
