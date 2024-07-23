using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using System;

public class AgentItem : MonoBehaviour
{
    [SerializeField]
    private EquippableItemSO item;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

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
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue,

                };
            }
        }
    }
}
