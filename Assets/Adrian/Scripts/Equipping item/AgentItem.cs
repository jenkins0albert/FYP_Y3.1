using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using System;
using System.Diagnostics.Contracts;
using System.Data.Common;
using TMPro;

public class AgentItem : MonoBehaviour
{
    [SerializeField]
    private EquippableItemSO item;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;
    public TextMeshProUGUI unequipText;

    public Transform itemSpawn;

    public string itemCurrentObject;
    public void SetItem(EquippableItemSO itemItemSO, List<ItemParameter> itemState)
    {

        
        DestroyAllChildrenImmediate(itemSpawn);
        if (item != null)
        {
            inventoryData.AddItem(item, 1, itemCurrentState);
               
        }


        /////////////////////////////


        unequipText.text = "Unequip [F]";
        this.item = itemItemSO;
        this.item.DefaultParametersList = itemItemSO.DefaultParametersList; 
        Debug.Log("current");
      
        this.itemCurrentState = new List<ItemParameter>(itemState);

        GameObject itemEquip = this.itemCurrentState[0].equipObject;

        Debug.Log("Item is called = " + itemCurrentState[0].itemName);

        GameObject instantiatedObject = Instantiate(itemEquip, Vector3.zero, Quaternion.identity);
        instantiatedObject.transform.rotation = itemEquip.transform.rotation;
        instantiatedObject.transform.SetParent(itemSpawn.transform, false);
        //////////////////////////////


        Debug.Log(itemSpawn.transform.position);
        //float newValue = itemCurrentState[index].value + parameter.value;

        ModifyParameter();
    }

    
    public void UnequipItem()
    {
        if (item != null)
        {
            DestroyAllChildrenImmediate(itemSpawn);
            unequipText.text = " ";
            inventoryData.AddItem(item, 1, itemCurrentState);
            item = null;
        }
    }
    public void DestroyAllChildrenImmediate(Transform itemNest)
    {
        foreach (Transform child in itemNest)
        {
            DestroyImmediate(child.gameObject);
        }
    }
    public void Start()
    {
        unequipText.text = " ";
    }

    public void Update()
    {
        itemCurrentObject = itemCurrentState[0].itemName;
    }
    private void ModifyParameter()
    {
        


        foreach (var parameter in parametersToModify)
        {
            if (itemCurrentState.Contains(parameter))
            {
                //itemCurrentObject = itemCurrentState[0].itemName;

                int index = itemCurrentState.IndexOf(parameter);
                
                
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    equipObject = parameter.equipObject,
                    //value = newValue,
                    itemName = parameter.itemName,
                    

                };
            }
        }
    }
    
    
}
