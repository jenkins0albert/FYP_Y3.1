using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUiAction : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;


    public void AddButtonUI(string name, Action onclickaction)
    {
        Debug.Log("AddButton");
        GameObject button = Instantiate(buttonPrefab, transform);
        button.GetComponent<Button>().onClick.AddListener(() => onclickaction());
        button.GetComponentInChildren<TMPro.TMP_Text>().text = name;


    }

    internal void Toggle(bool val)
    {
        if (val == true)
        {
            RemoveOldButtons();
            Debug.Log("removebuttons");
        }
        gameObject.SetActive(val);
    }

    private void RemoveOldButtons()
    {
        foreach (Transform transformChildObjects in transform)
        {
            Destroy(transformChildObjects.gameObject);
        
        }
        
    }
}
