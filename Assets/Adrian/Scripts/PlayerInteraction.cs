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

    
    public bool bagCollected = false;

    [SerializeField]
    private InventorySO inventoryData;

    public float PlayerReach = 3f;
    Interactable currentInteractable;
    

    private GameObject _mainCamera;

    
    private DialogueManager dialogueCheck;
    public GameObject dialogueManager;

    public TextMeshProUGUI hoverText;

    public GameObject bagUI;

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

                    currentInteractable.DisableOutline();

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
        
        currentInteractable.EnableOutline();
        
        

    }

    void DisableInteraction()
    {
        if (currentInteractable)
        {
            
            currentInteractable.DisableOutline();
            
            currentInteractable = null;
            hoverText.text = "";
        }
    }

    
    public void OnInventory()
    {
        if (bagCollected)
        {
            inventoryControls.ShowInventory();
            Debug.Log("sada");
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


        //////////////////////////////
        if (bagCollected == true)
        {
            bagUI.SetActive(true);
        }
        if (inventoryControls.InventoryOpen == true)
        {
            bagUI.SetActive(false);
        }
        /////////////////////////////

    }


    
}
