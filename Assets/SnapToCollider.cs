using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToCollider : MonoBehaviour
{
    public GameObject correctPuzzlePiece; // The correct puzzle piece for this collider
    public Vector3 snapPosition; // The correct position to snap the puzzle piece
    public float snapRotationY; // The correct Y-axis rotation to snap the puzzle piece
    public float snapDistance = 1.0f; // The maximum distance within which snapping can occur

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == correctPuzzlePiece)
        {
            float distance = Vector3.Distance(snapPosition, other.transform.position);

            if (distance <= snapDistance)
            {
                // Snap the puzzle piece to the correct position
                other.transform.position = snapPosition;

                // Snap the puzzle piece to the correct Y-axis rotation
                Vector3 currentRotation = other.transform.eulerAngles;
                other.transform.rotation = Quaternion.Euler(currentRotation.x, snapRotationY, currentRotation.z);

                // Debug logs to check alignment
                Debug.Log($"{correctPuzzlePiece.name} snapped to {snapPosition} with Y rotation {snapRotationY}");
            }
            else
            {
                // Debug log to show distance
                Debug.Log($"{correctPuzzlePiece.name} is {distance} units away from the snap position");
            }
        }
    }
}
