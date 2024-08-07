using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private bool isDragging = false;
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
            // Update the puzzle piece's position based on the mouse position
            Vector3 mousePos = GetMouseWorldPos();
            transform.position = new Vector3(mousePos.x, transform.position.y, mousePos.z);

            // Rotate the piece with right mouse button while dragging
            if (Input.GetMouseButton(1))  // Use GetMouseButton to keep rotating while holding the button
            {
                transform.Rotate(0, 45 * Time.deltaTime, 0);  // Rotate smoothly
            }
        }
    }

    Vector3 GetMouseWorldPos()
    {
        // Get the mouse position in screen space
        Vector3 mousePoint = Input.mousePosition;
        // Set the z position to match the puzzle piece's z position
        mousePoint.z = mainCamera.WorldToScreenPoint(transform.position).z;
        // Convert to world space
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
