using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    //[SerializeField]
    //private AudioSource audioSource;

    [SerializeField]
    private float duration = 0.3f;

    [SerializeField]
    private Sprite itemImage;

    private void Start()
    {
        itemImage = InventoryItem.sprite;
    }

    public void DestroyItem()
    {
        
        StartCoroutine(AnimateItemPickup());

    }

    private IEnumerator AnimateItemPickup()
    {
        //audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale =
                Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
