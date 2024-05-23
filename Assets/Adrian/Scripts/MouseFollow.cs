using System.Collections;
using System.Collections.Generic;
using Inventory.UI;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private Camera maincam;

    [SerializeField]
    private UIInventoryItem uIInventoryItem;



    public void Awake()
    {
        canvas= transform.parent.GetComponent<Canvas>();
        maincam = Camera.main;
        uIInventoryItem = GetComponentInChildren<UIInventoryItem>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
        uIInventoryItem.SetData(sprite, quantity); 
    }

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                (RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out position

            );

        transform.position = canvas.transform.TransformPoint(position);
        
    }

    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive( val );
    }
}
