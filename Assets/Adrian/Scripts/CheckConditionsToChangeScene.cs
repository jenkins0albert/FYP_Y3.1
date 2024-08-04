using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CheckConditionsToChangeScene : MonoBehaviour
{
    private DialogueTrigger dialogue;

    [SerializeField]
    private AgentItem itemcheck;

    [SerializeField]
    private SceneChanger changer;

    private void Start()
    {
        dialogue = this.GetComponent<DialogueTrigger>();
        itemcheck = FindObjectOfType<AgentItem>();

        //Debug.Log(itemcheck.itemCurrentObject);
    }
    private void Update()
    {
        //Debug.Log("From check conditions =" + itemcheck.itemCurrentObject);
    }
    public void CheckConditions()
    {
        if (itemcheck.itemCurrentObject == "Key")
        {
            changer.ChangeScene();
            itemcheck.UnequipItem();
        }

        else
        {
            dialogue.TriggerDialogue();
        }
    }
}
