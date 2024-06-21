using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteInteractable : MonoBehaviour
{
    [SerializeField]
    private PlayerInteraction playerInteraction;

    [SerializeField]
    private List<GameObject> interactable1;

    public void Awake()
    {

        
        

        
    }
    public void InteractionForCube()
    {
        DeleteInteraction(0);
    }

    public void DeleteInteraction(int count) 
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        Interactable[] interactable = interactable1[count].GetComponents<Interactable>();
        

        if (playerInteraction.bagCollected == true)
        {
            Destroy(interactable[0]);
        }
        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
