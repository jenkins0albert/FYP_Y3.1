using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneEnable : MonoBehaviour
{
    

    [SerializeField]
    private int sceneNum;

    [SerializeField]
    private float duration = 0.4f;

    [SerializeField]
    private GameObject transitionIn;

    [SerializeField]
    private GameObject transitionOut;

    

    private IEnumerator AnimateTransition(int sceneNum)
    {

        yield return new WaitForSeconds(duration);
        SceneManager.LoadSceneAsync(sceneNum);



    }


    public void OnEnable()
    {
        
        StartCoroutine(AnimateTransition(sceneNum));
    }

   
}
