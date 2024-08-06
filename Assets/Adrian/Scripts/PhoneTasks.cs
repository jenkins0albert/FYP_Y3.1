using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneTasks : MonoBehaviour
{
    [SerializeField]
    private GameObject phone;

    [SerializeField]
    private TextMeshProUGUI phoneText;

    [SerializeField]
    [TextArea(3, 10)]
    private string task;

    private void Start()
    {
        phone = GameObject.Find("Screen");
        phoneText = phone.GetComponent<TextMeshProUGUI>();
        phoneText.text = task;
    }


}
