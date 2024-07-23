using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Inventory.UI
{
    public class UIInventory : MonoBehaviour

    {

        [SerializeField]
        private UIInventoryItem prefabItem;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private UIInventoryDescription inventoryItemUIDescription;

        [SerializeField]
        private MouseFollow mouseFollow;

        List<UIInventoryItem> listOfItems = new List<UIInventoryItem>();

        public event Action<int> OnDescriptionRequested, OnItemRequested, OnStartDragging;
        public event Action<int, int> OnSwapItems;

        private int currentlyDraggedInt = -1;
        private void Awake()
        {

            Hide();
            mouseFollow.Toggle(false);
            inventoryItemUIDescription.ResetDescription();

            
        }

        public void Show()
        {
            gameObject.SetActive(true);

            ResetSelection();

        }
        public void ResetSelection()
        {
            inventoryItemUIDescription.ResetDescription();
            DeselectAllItems();
        }

        public void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listOfItems)
            {
                item.Deselect();
            }
        }
        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();

        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfItems.Count > itemIndex)
            {
                listOfItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        public void InitializeInventoryItem(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem uiItem = Instantiate(prefabItem, Vector2.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                listOfItems.Add(uiItem);


                uiItem.OnSelectItem += HandleItemSelection;
                uiItem.OnItemDragged += HandleBeginDrag;
                uiItem.OnItemDropOn += HandleSwap;
                uiItem.OnItemDragEnd += HandleEndDrag;
                uiItem.OnRightSelectItem += HandleShowItemAction;

            }


        }

        private void HandleShowItemAction(UIInventoryItem inventoryItemUI)
        {
            int index = listOfItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {

                return;
            }

            OnItemRequested?.Invoke(index);
        }

        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();


        }

        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = listOfItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {

                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedInt, index);
            HandleItemSelection(inventoryItemUI);
        }

        private void ResetDraggedItem()
        {
            mouseFollow.Toggle(false);
            currentlyDraggedInt = -1;
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = listOfItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            currentlyDraggedInt = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);



        }

        public void CreateDraggedItem(Sprite itemImg, int quantity)
        {
            mouseFollow.Toggle(true);
            mouseFollow.SetData(itemImg, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = listOfItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }

            OnDescriptionRequested?.Invoke(index);

        }

        internal void UpdateDescription(int itemIndex, Sprite sprite, string name, string description)
        {
            inventoryItemUIDescription.SetDescription(sprite, name, description);
            DeselectAllItems();
            listOfItems[itemIndex].Select();


        }

        internal void ResetAllItems()
        {
            foreach(var item in listOfItems) 
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}