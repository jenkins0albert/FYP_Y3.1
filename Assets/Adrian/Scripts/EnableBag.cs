using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBag : MonoBehaviour
{
    
    public PlayerInteraction playerInteraction;

    // Start is called before the first frame update
    public void BagCollected()
    {
       playerInteraction.bagCollected = true;
    }
    public void Start()
    {
        PlayerInteraction playerInteraction = GetComponent<PlayerInteraction>();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
