using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteInteractable : MonoBehaviour
{
    

    [SerializeField]
    private List<GameObject> interactable1;

    [SerializeField]
    private ItemPickup pickup;

    public bool bagCollected = false;
    public bool keyCollected = false;

    public void Awake()
    {

        
        

        
    }
    public void InteractionForCube()
    {
        DeleteInteraction(0);
    }

    

    public void CollectBagForKey()
    {
        bagCollected = true;
        DeleteInteraction(0);
    }

    public void CollectKeyForWallet()
    {
        if (bagCollected != false)
        {
            keyCollected = true;
            DeleteInteraction(1);
            pickup.DestroyItem();
        }

        else 
        {
            Debug.Log("");
                
        }
        
    }

    public void CollectWalletForDoor()
    {
        DeleteInteraction(2);
    }
    public void DeleteInteraction(int count) 
    {
        
        Interactable[] interactable = interactable1[count].GetComponents<Interactable>();
        

        
        Destroy(interactable[0]);
        
        

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
