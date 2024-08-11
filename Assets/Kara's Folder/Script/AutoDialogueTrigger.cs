using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDialogueTrigger : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;  // Reference to the DialogueTrigger component

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger
        if (other.CompareTag("Player"))  
        {
            // Check if dialogueTrigger is assigned
            if (dialogueTrigger != null)
            {
                // Trigger the dialogue
                dialogueTrigger.TriggerDialogue();
            }
            else
            {
                Debug.LogWarning("DialogueTrigger is not assigned.");
            }
        }
    }
}
