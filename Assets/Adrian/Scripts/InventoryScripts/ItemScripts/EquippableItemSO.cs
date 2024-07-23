using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Inventory.Model
{

    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyable, IItemAction
    {
        public string ActionName => "Equip";

        public AudioClip actionSFX { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)  
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IDestroyable
    {

    }

    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip actionSFX { get; }
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }
}
