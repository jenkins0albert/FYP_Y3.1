using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using System;
using System.Diagnostics.Contracts;
using System.Data.Common;

public class AgentItem : MonoBehaviour
{
    [SerializeField]
    private EquippableItemSO item;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;


    public Transform itemSpawn;

    public void SetItem(EquippableItemSO itemItemSO, List<ItemParameter> itemState)
    {
        if (item != null)
        {
            inventoryData.AddItem(item, 1, itemCurrentState);
               
        }
        this.item = itemItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        
        ModifyParameter();
    }

   

    

    private void ModifyParameter()
    {
        
        foreach (var parameter in parametersToModify)
        {
            if (itemCurrentState.Contains(parameter))
            {

                GameObject itemNest = GameObject.Find("NestItemInventory");
                int index = itemCurrentState.IndexOf(parameter);
                GameObject itemEquip = parameter.equipObject;

                

                GameObject instantiatedObject = Instantiate(itemEquip, Vector3.zero, Quaternion.identity);

                instantiatedObject.transform.SetParent(itemSpawn.transform, false);

                

                Debug.Log(itemSpawn.transform.position);
                //float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    
                    //value = newValue,

                };
            }
        }
    }
    
    
}
