using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private bool isDragging = false;
    private float originalY;
    private Rigidbody rb;

    [SerializeField]
    private Camera puzzleCamera;  // Reference to the puzzle camera

    [SerializeField]
    private float pickUpHeight = 0.1f;

    void Start()
    {
        // Find the puzzle camera by name
        puzzleCamera = GameObject.Find("PuzzleCamera")?.GetComponent<Camera>();

        if (puzzleCamera == null)
        {
            Debug.LogError("PuzzleCamera not found in the scene.");
            puzzleCamera = Camera.main;  // If puzzle camera isn't found, it will go back to the main camera (Player)
        }

        originalY = transform.position.y;
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        isDragging = true;
        // Lift the puzzle piece when picked up
        transform.position = new Vector3(transform.position.x, originalY + pickUpHeight, transform.position.z);

        // Reset rotation on X and Z axes
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

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
            if (Input.GetMouseButton(1))  // Use RMB to keep rotating while holding the button
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
        mousePoint.z = puzzleCamera.WorldToScreenPoint(transform.position).z;

        // Convert to world space using the puzzle camera
        return puzzleCamera.ScreenToWorldPoint(mousePoint);
    }
}
