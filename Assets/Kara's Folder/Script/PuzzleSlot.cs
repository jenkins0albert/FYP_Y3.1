using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    public Transform correctPuzzlePiece;
    private float rotationTolerance = 0.7f; // Adjust this value if necessary

    [SerializeField]
    private bool isCorrect = false;

    [SerializeField]
    private QuestManager questManager; // Reference to the Quest Manager

    void OnTriggerStay(Collider other)
    {
        if (other.transform == correctPuzzlePiece)
        {
            float angleDifference = Mathf.Abs(Mathf.DeltaAngle(other.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y));

            if (angleDifference <= rotationTolerance)
            {
                // Snap the puzzle piece to the slot
                other.transform.position = transform.position;
                other.transform.rotation = transform.rotation;

                // Get the Rigidbody and set it to kinematic
                Rigidbody rb = other.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }

                // Set isCorrect to true
                if (!isCorrect)
                {
                    isCorrect = true;

                    // Remove the PuzzlePiece script to prevent further dragging
                    PuzzlePiece piece = other.GetComponent<PuzzlePiece>();
                    if (piece != null)
                    {
                        Destroy(piece);  // Remove the script component
                    }

                    // Notify the Quest Manager that this puzzle slot is completed
                    if (questManager != null)
                    {
                        questManager.CheckPuzzleCompletion();
                    }
                }
            }
        }
    }

    public bool IsCorrect()
    {
        return isCorrect;
    }

}
