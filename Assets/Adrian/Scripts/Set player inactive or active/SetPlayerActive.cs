using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerActive : MonoBehaviour
{
    [SerializeField]
    private SetPlayerInactive inactive;

    
    
    private void OnEnable()
    {
        Debug.Log("adshvsakbdsad");
        inactive.EnablePlayers();

    }

    
}
