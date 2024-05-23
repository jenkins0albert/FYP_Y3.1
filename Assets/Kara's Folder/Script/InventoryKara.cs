using System.Collections.Generic;
using UnityEngine;

public class InventoryKara : MonoBehaviour
{
    private Dictionary<string, string> items = new Dictionary<string, string>(); // dictionary to store items

    public void AddItem(string itemID, string itemName)
    {
        items.Add(itemID, itemName);
        UpdateInventory();
    }

    private void UpdateInventory()
    {
        Debug.Log("Inventory:");
        int slotCount = 4; // no. of slots in the inventory
        for (int i = 0; i < slotCount; i++)
        {
            string item = "None";
            if (items.ContainsKey(i.ToString()))
            {
                item = items[i.ToString()];
            }
            Debug.Log($"Slot {i + 1}: {item}");
        }
    }
}
