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
using System;


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

    public bool phoneOpen = false;
    public GameObject phone;
    public TextMeshProUGUI phoneText;

    public GameObject MenuUI;
    public GameObject confirmQuit;
    public bool MenuOpen = false;

    public void OnOptions()
    {
        if (MenuOpen == false)
        {
            MenuUI.SetActive(true);
            confirmQuit.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            MenuOpen = true;
            Time.timeScale = 0;
        }

        else 
        {
            MenuOpen = false;
            MenuUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
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

    public void OnPhone()
    {
        if (phoneOpen == false)
        {
            phoneOpen = true;
            phone.SetActive(true);
            phone.GetComponent<Animator>().Play("phoneUP");
        }

        else
        {
            phoneOpen = false;
            
            StartCoroutine(PhoneActive());
            
        }
    }

    private IEnumerator PhoneActive()
    {
        phone.GetComponent<Animator>().Play("phoneDOWN");
        yield return new WaitForSeconds(0.1f);
        phone.SetActive(false);



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


               if (newInteractable.isActiveAndEnabled)
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
