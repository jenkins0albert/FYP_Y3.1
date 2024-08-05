using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    public Transform correctPuzzlePiece;
    private bool isCorrect = false;
    private float rotationTolerance = 1f; // Adjust this value if necessary

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger Stay"); // Debug statement

        if (other.transform == correctPuzzlePiece)
        {
            Debug.Log("Correct Puzzle Piece Entered"); // Debug statement

            float angleDifference = Mathf.Abs(Mathf.DeltaAngle(other.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y));
            if (angleDifference <= rotationTolerance)
            {
                Debug.Log("Correct Rotation"); // Debug statement

                other.transform.position = transform.position;
                other.transform.rotation = transform.rotation; // Ensure the rotation is also set correctly
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }
                other.GetComponent<PuzzlePiece>().enabled = false;
                Collider col = other.GetComponent<Collider>();
                if (col != null)
                {
                    col.enabled = false; // Disable the collider to prevent further dragging
                }
                isCorrect = true;
                Debug.Log("Puzzle Piece Locked"); // Debug statement
            }
            else
            {
                Debug.Log("Incorrect Rotation"); // Debug statement
                Debug.Log($"Angle Difference: {angleDifference}"); // Debug statement
            }
        }
        else
        {
            Debug.Log("Incorrect Puzzle Piece"); // Debug statement
        }
    }
}





