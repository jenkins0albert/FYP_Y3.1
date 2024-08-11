using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public TextMeshProUGUI charactername;
    public TextMeshProUGUI dialoguearea;

    private Queue<DialogueLines> lines;
    private Queue<AudioClip> sounds;  // Kara's Changes: Queue to hold the sounds

    [SerializeField]
    private FirstPersonController playerController;

    public bool isDialogueactive = false;
    public float typingspeed = 0.05f;
    public Animator animator;

    private AudioSource audioSource;  // Kara's Changes: Reference to the AudioSource component

    private void Awake()
    {
        if (instance == null)
            instance = this;

        lines = new Queue<DialogueLines>();
        sounds = new Queue<AudioClip>();
        audioSource = GetComponent<AudioSource>();

        Debug.Log("AudioSource component: " + (audioSource != null ? "Found" : "Not found"));
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueactive = true;
        animator.Play("DialogueIn");

        lines.Clear();
        sounds.Clear();

        Debug.Log("Starting dialogue with " + dialogue.dialoguelines.Count + " lines and " + dialogue.dialogueSounds.Count + " sounds.");

        foreach (DialogueLines dialogueline in dialogue.dialoguelines)
        {
            lines.Enqueue(dialogueline);
        }

        foreach (AudioClip sound in dialogue.dialogueSounds)
        {
            sounds.Enqueue(sound);
        }

        DisplayNext();
    }

    public void Update()
    {
        if (isDialogueactive)
        {
            playerController.MoveSpeed = 0f;
            playerController.SprintSpeed = 0f;
            playerController.JumpHeight = 0f;
        }
    }

    public void DisplayNext()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLines currentline = lines.Dequeue();
        charactername.text = currentline.character.name;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentline));

        if (sounds.Count > 0)
        {
            AudioClip soundToPlay = sounds.Dequeue();
            Debug.Log("Playing sound: " + (soundToPlay != null ? soundToPlay.name : "No sound"));

            if (audioSource != null && soundToPlay != null)
            {
                audioSource.clip = soundToPlay;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource or AudioClip is null.");
            }
        }
        else
        {
            Debug.LogWarning("No sounds left in the queue.");
        }
    }

    IEnumerator TypeSentence(DialogueLines dialoguelines)
    {
        dialoguearea.text = " ";

        foreach (char letter in dialoguelines.line.ToCharArray())
        {
            dialoguearea.text += letter;
            yield return new WaitForSeconds(typingspeed);
        }
    }

    void EndDialogue()
    {
        isDialogueactive = false;
        animator.Play("DialogueOut");

        playerController.MoveSpeed = 4f;
        playerController.SprintSpeed = 6.0f;
        playerController.JumpHeight = 0.0f;
    }
}
