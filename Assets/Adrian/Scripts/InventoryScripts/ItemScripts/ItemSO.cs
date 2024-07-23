using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public abstract class ItemSO : ScriptableObject
    {
        public int myProperty { get; set; }

        [field: SerializeField]
        public bool isStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int maxStackSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite sprite { get; set; }

        [field: SerializeField]
        public List<ItemParameter> DefaultParametersList { get; set; }


    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameters itemParameter;
        public float value;
        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}