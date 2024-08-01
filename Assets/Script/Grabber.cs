using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    private GameObject selectedObject;
    private float liftHeight = 0.5f; // Amount to lift the object
    private float rotationAngle = 45f; // Angle to rotate the object

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for mouse button down event
        {
            if (selectedObject == null) // When no object is selected
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null && hit.collider.CompareTag("Drag")) // Check if the hit object has the "Drag" tag
                {
                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;
                    LiftObject(selectedObject, liftHeight); // Lift the object
                }
            }
        }

        if (Input.GetMouseButton(0) && selectedObject != null) // Check for mouse button hold and object selected
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, selectedObject.transform.position.y, worldPosition.z); // Keep the y position as it is

            if (Input.GetMouseButtonDown(1)) // Check for right mouse button down event
            {
                RotateObject(selectedObject, rotationAngle); // Rotate the object
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedObject != null) // Check for mouse button up event
        {
            DropObject(selectedObject); // Drop the object back down
            selectedObject = null;
            Cursor.visible = true;
        }
    }

    private RaycastHit CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit;
    }

    private void LiftObject(GameObject obj, float height)
    {
        obj.transform.position += new Vector3(0, height, 0); // Lift the object by the specified height
    }

    private void DropObject(GameObject obj)
    {
        obj.transform.position -= new Vector3(0, liftHeight, 0); // Drop the object back down by the specified height
    }

    private void RotateObject(GameObject obj, float angle)
    {
        obj.transform.Rotate(0, angle, 0); // Rotate the object by the specified angle around the Y-axis
    }
}
