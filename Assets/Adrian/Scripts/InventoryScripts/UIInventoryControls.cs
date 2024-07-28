using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.UI;
using UnityEngine;
using Inventory.Model;
using UnityEngine.Analytics;

namespace Inventory
{
    public class UIInventoryControls : MonoBehaviour
    {
        [SerializeField]
        private UIInventory uiInventory;

        [SerializeField]
        private GameObject bagUI;

        [SerializeField]
        private GameObject phoneUI;

        [SerializeField]
        private GameObject optionsUI;



        public bool InventoryOpen = false;
        [SerializeField]
        private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
           
            foreach (InventoryItem item in initialItems)
            {
                if (item.item != null)
                {
                    if (item.item.Name == "Key" && item.quantity > 0)
                    {
                        Debug.Log("?");
                    }
                }
                
            }
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;

            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                {
                    continue;

                }
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            uiInventory.ResetAllItems();
            foreach (var item in inventoryState)
            {
                uiInventory.UpdateData(item.Key, item.Value.item.sprite, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            uiInventory.InitializeInventoryItem(inventoryData.size);

            uiInventory.OnDescriptionRequested += HandleDescriptionRequest;
            uiInventory.OnSwapItems += HandleSwapItems;
            uiInventory.OnStartDragging += HandleDragging;
            uiInventory.OnItemRequested += HandleItemRequest;

            


        }

        private void HandleItemRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);

            if (inventoryItem.IsEmpty)
                return;


            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                
                uiInventory.ShowItemAction(itemIndex);
                uiInventory.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));

            }

            IDestroyable destroyableItem = inventoryItem.item as IDestroyable;
            if (destroyableItem != null)
            {
                Debug.Log("destroy");
                //inventoryData.RemoveItem(itemIndex, 1);
            }


        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);

            if (inventoryItem.IsEmpty)
                return;

            IDestroyable destroyableItem = inventoryItem.item as IDestroyable;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                {
                    uiInventory.ResetSelection();
                }
                    
            }
        }
       

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            { 
                return; 
            }

            uiInventory.CreateDraggedItem(inventoryItem.item.sprite, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                uiInventory.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;

            uiInventory.UpdateDescription(itemIndex, item.sprite, item.Name, item.Description);

        }

        public void ShowInventory()
        {
            if (InventoryOpen == false)
            {
                InventoryOpen = true;

                bagUI.SetActive(false);
                phoneUI.SetActive(false);

                optionsUI.SetActive(false);

                uiInventory.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    uiInventory.UpdateData(item.Key, item.Value.item.sprite, item.Value.quantity);

                }
                Cursor.lockState = CursorLockMode.None;

            }

            else
            {
                bagUI.SetActive(true);
                phoneUI.SetActive(true);
                optionsUI.SetActive(true);
                InventoryOpen = false;
                uiInventory.Hide();
                Cursor.lockState = CursorLockMode.Locked;
            }
            Debug.Log("sada");
        }
    }
}