using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text itemTitle;
        [SerializeField]
        private TMP_Text itemDescription;

        public void Awake()
        {
            ResetDescription();

        }

        public void ResetDescription()
        {
            itemImage.gameObject.SetActive(false);
            itemTitle.text = "";
            itemDescription.text = "";
        }

        public void SetDescription(Sprite sprite, string itemName, string itemDescription)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;

            itemTitle.text = itemName;
            this.itemDescription.text = itemDescription;
        }
    }
}