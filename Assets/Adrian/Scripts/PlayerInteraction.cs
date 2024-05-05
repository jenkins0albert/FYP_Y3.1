using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.Arm;
using UnityEngine.SceneManagement;


public class PlayerInteraction : MonoBehaviour
{

    public float PlayerReach = 3f;
    Interactable currentInteractable;
    

    private GameObject _mainCamera;


    private DialogueManager dialogueCheck;
    public GameObject dialogueManager;

    public TextMeshProUGUI hoverText;


    public void OnInteract()
    {



        if (dialogueCheck.isDialogueactive == true)
        {
            dialogueCheck.DisplayNext();
        }
        else 
        {
            InteractObject();
        }


    }
    // Update is called once per frame
    public void InteractObject()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();

        }
    }
    void checkInteraction()
    {
       

        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, PlayerReach))
        {
           

            if (hit.collider.tag == "Interactable")
            {
                


                
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                


                if (currentInteractable && newInteractable != currentInteractable)
                {
                    Debug.Log("DisableOutline");
                    hoverText.text = " ";
                }

                if (newInteractable.enabled) 
                {
                    SetNewCurrentInteractable(newInteractable);
                    hoverText.text = newInteractable.hoverMsg;

                    if (dialogueCheck.isDialogueactive == true)
                    {
                        hoverText.text = " ";
                    }
                }


                else
                {
                    DisableInteraction();
                    hoverText.text = " ";
                }

            }


        }

        else
        {
            DisableInteraction();
            hoverText.text = " ";
        }


    }


    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;


    }

    void DisableInteraction()
    {
        if (currentInteractable)
        {
            currentInteractable = null;
            hoverText.text = " ";
        }
    }


    void Start()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        dialogueCheck = dialogueManager.GetComponent<DialogueManager>();

        hoverText.text = " ";

    }

    
    private void Awake()
    {
        hoverText.text = " ";
    }

    void Update()
    {
        checkInteraction();

        

    }


    
}
