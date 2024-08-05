using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private float originalY;
    private float pickUpHeight = 0.5f;
    private Rigidbody rb;

    void Start()
    {
        mainCamera = Camera.main;
        originalY = transform.position.y;
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
        // Lift the puzzle piece when picked up
        transform.position = new Vector3(transform.position.x, originalY + pickUpHeight, transform.position.z);
        // Disable gravity and make it kinematic
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        // Drop the puzzle piece back to the original Y position
        transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
        // Enable gravity and make it non-kinematic
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = GetMouseWorldPos() + offset;
            transform.position = new Vector3(mousePos.x, transform.position.y, mousePos.z);

            // Rotate the piece with right mouse button while dragging
            if (Input.GetMouseButtonDown(1))
            {
                transform.Rotate(0, 45, 0);
            }
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
