using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private GameObject objectiveItem; // The item that should appear after all puzzles are completed

    [SerializeField]
    private PuzzleSlot[] puzzleSlots; // Array of puzzle slots to track completion

    [SerializeField]
    private GameObject interactableObject; // The object to be tagged as "Untagged" after completing the quest

    private int completedPuzzles = 0;

    void Start()
    {
        if (objectiveItem != null)
        {
            objectiveItem.SetActive(false); // Hide the objective item initially
        }
    }

    // Call this method when a puzzle slot is completed
    public void CheckPuzzleCompletion()
    {
        completedPuzzles = 0; // Reset the count

        foreach (PuzzleSlot slot in puzzleSlots)
        {
            if (slot.IsCorrect())
            {
                completedPuzzles++;
            }
        }

        // Check if all puzzle slots are completed
        if (completedPuzzles >= puzzleSlots.Length)
        {
            ShowObjectiveItem();
            ChangeInteractableTag();
        }
    }

    private void ShowObjectiveItem()
    {
        if (objectiveItem != null)
        {
            objectiveItem.SetActive(true); // Show the objective item
        }
    }

    private void ChangeInteractableTag()
    {
        if (interactableObject != null)
        {
            interactableObject.tag = "Untagged";
        }
    }
}
