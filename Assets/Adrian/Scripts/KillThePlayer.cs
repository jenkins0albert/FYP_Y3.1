using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillThePlayer : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();  
    }

    public void KillPlayer()
    {
        gameManager.GameOver();
    }
}
