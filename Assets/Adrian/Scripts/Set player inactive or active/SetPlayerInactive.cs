using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerInactive : MonoBehaviour
{
    [SerializeField]
    private PlayerInteraction playerInteraction;

    [SerializeField]
    private GameManager gameManager;
    public void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        gameManager = FindObjectOfType<GameManager>();

        playerInteraction.gameObject.SetActive(false);
        gameManager.gameObject.SetActive(false);



    }

    public void EnablePlayers()
    {
        playerInteraction.gameObject.SetActive(true);
        gameManager.gameObject.SetActive(true);
    }



    public void SetInactive()
    {
        
    }

    
}
