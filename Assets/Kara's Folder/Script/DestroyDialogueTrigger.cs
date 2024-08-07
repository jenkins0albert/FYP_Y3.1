using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDialogueTrigger : MonoBehaviour //This is to destroy the dialogue trigger (meant for interactables where you interact ONCE)
{
    [SerializeField]
    private DialogueTrigger dialogueTrigger;

    public void DestroyDialogue()
    {
        Destroy(dialogueTrigger);
    }

}
