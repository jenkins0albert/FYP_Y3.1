using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public Animator transition;

    [SerializeField]
    private int sceneNum;

    [SerializeField]
    private float duration = 0.4f;


    public void ChangeScene()
    {
        StartCoroutine(AnimateTransition(sceneNum));
    }

    private IEnumerator AnimateTransition(int sceneNum)
    {
        transition.Play("New Animation");

        yield return new WaitForSeconds(duration);
        SceneManager.LoadSceneAsync(sceneNum);
        


    }

    public void SceneTwo()
    {
        SceneManager.LoadSceneAsync(0);
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
