using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
}

[System.Serializable]
public class DialogueLines
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLines> dialoguelines = new List<DialogueLines>();

    // Kara's Changes
    public List<AudioClip> dialogueSounds = new List<AudioClip>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        if (DialogueManager.instance != null)
        {
            // Debug log for ensuring the method is called
            DialogueManager.instance.StartDialogue(dialogue);
        }
        else
        {
            // Debug log for identifying missing DialogueManager
            Debug.LogWarning("DialogueManager instance is null.");
        }
    }
}
