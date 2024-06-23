using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitTheGame : MonoBehaviour
{

    public void Quit()
    {
        
        Application.Quit();

        Debug.Log("QUIT GAME");

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
