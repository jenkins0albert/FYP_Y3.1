using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnableBag : MonoBehaviour
{
    [SerializeField]
    private PlayerInteraction playerInteraction;

    // Start is called before the first frame update
    

    public void OnEnable()
    {
        Debug.Log("sceneloaded");
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    public void Awake()
    {
        PlayerInteraction playerInteraction = GetComponent<PlayerInteraction>();

    }
    public void BagCollected()
    {
        playerInteraction.bagCollected = true;
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
