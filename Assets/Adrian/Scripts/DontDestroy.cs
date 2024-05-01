using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DontDestroy : MonoBehaviour
{
    

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void EnableText()
    { 

    }

    public void DisableText() 
    {
        
    }
}
