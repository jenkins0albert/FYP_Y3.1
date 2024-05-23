using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private TMP_Text quantityText;


        [SerializeField]
        private Image borderImage;

        public event Action<UIInventoryItem> OnItemDropOn, OnItemRemoved, OnItemDragged, OnItemDragEnd, OnSelectItem, OnRightSelectItem;

        private bool empty = true;

        public void Awake()
        {

            Deselect();
            ResetData();

        }

        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            empty = true;

        }
        public void Select()
        {
            borderImage.enabled = true;
        }
        public void Deselect()
        {
            borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityText.text = quantity + "";
            empty = false;
        }






        /// <summary>
        /// /
        /// </summary>
        /// <param name="eventData"></param>

        public void OnPointerClick(PointerEventData pointerData)
        {



            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightSelectItem?.Invoke(this);

            }

            else
            {
                OnSelectItem?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty) return;
            OnItemDragged?.Invoke(this);

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemDragEnd?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDropOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
    }
}