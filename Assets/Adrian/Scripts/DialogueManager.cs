using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;

    public TextMeshProUGUI charactername;
    public TextMeshProUGUI dialoguearea;

    private Queue<DialogueLines> lines;

    [SerializeField]
    private FirstPersonController playerController;

    public bool isDialogueactive = false;
    public float typingspeed = 0.05f;
    public Animator animator;

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueactive = true;
        animator.Play("DialogueIn");

        

        lines.Clear();

        foreach (DialogueLines dialogueline in dialogue.dialoguelines)
        {
            lines.Enqueue(dialogueline);

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
        playerController.JumpHeight = 0.7f;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        lines = new Queue<DialogueLines>();
    }

}
