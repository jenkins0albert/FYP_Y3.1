using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;





public class Interactable : MonoBehaviour
{
    Outline outline;
    public string hoverMsg;
    public UnityEvent onInteraction;

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();


            
    }
    public void Interact()
    {
        onInteraction.Invoke();
        

    }
    public void DisableOutline()
    {
        if (this.gameObject != null)
        {
            outline.enabled = false;
        }
       
    }

    public void EnableOutline()
    {
        if (this.gameObject != null)
        {
            outline.enabled = true;
        }
    }
    

    
}
