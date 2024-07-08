using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class DisableTriggerDialogue : MonoBehaviour
{
    
    public EnableBag bagcollected;
    public DialogueTrigger trigger;

    

    [SerializeField]
    private PlayerInteraction playerInteraction;


    public void OnEnable()
    {
        Debug.Log("sceneloaded");
        playerInteraction = FindObjectOfType<PlayerInteraction>();

        Interactable interactable = this.GetComponent<Interactable>();
        //UnityEventTools.RemovePersistentListener(interactable.onInteraction, 0);

    }

    

    // Start is called before the first frame update
    public void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
