using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items
{
    public enum ItemType
    {
        ID,
        Key
    }

    public ItemType itemType;
    public int amount;
}
