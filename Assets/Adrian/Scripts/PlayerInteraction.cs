using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.Arm;
using UnityEngine.SceneManagement;
using Inventory;
using Inventory.UI;
using Inventory.Model;
using static UnityEditor.Progress;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    public float PlayerReach = 3f;
    Interactable currentInteractable;
    

    private GameObject _mainCamera;

    
    private DialogueManager dialogueCheck;
    public GameObject dialogueManager;

    public TextMeshProUGUI hoverText;



    [SerializeField]
    private UIInventory inventoryUI;
    [SerializeField]
    private UIInventoryControls inventoryControls;
    
    
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

            ItemPickup item = currentInteractable.GetComponent<ItemPickup>();
            if (item != null)
            {
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                {
                    currentInteractable.Interact();
                }

                else
                {
                    item.Quantity = reminder;
                }

            }





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
            hoverText.text = "";
        }
    }

    
    public void OnInventory()
    {
        inventoryControls.ShowInventory();
        Debug.Log("sada");
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
