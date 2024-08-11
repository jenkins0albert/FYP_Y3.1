using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyOnTrigger : MonoBehaviour
{
    [SerializeField]
    private DestroyDialogueTrigger destroyDialogueTrigger; // Reference to the DestroyDialogueTrigger script

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger
        if (other.CompareTag("Player"))  
        {
            // Check if the destroyDialogueTrigger reference is assigned
            if (destroyDialogueTrigger != null)
            {
                // Call the DestroyDialogue method to destroy the DialogueTrigger
                destroyDialogueTrigger.DestroyDialogue();
            }
            else
            {
                Debug.LogWarning("DestroyDialogueTrigger reference is not assigned.");
            }

            //Destroy the trigger itself 
            Destroy(gameObject);
        }
    }
}
