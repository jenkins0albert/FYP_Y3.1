using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;





public class Interactable : MonoBehaviour
{
    
    public string hoverMsg;
    public UnityEvent onInteraction;

    public void Interact()
    {
        onInteraction.Invoke();

    }
    void Start()
    {
        
    }

    
}
