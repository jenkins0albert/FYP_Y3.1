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

    [SerializeField]
    private GameObject transitionIn;

    [SerializeField]
    private GameObject transitionOut;

    public void ChangeScene()
    {
        transitionOut.SetActive(true);
        StartCoroutine(AnimateTransition(sceneNum));
    }

    private IEnumerator AnimateTransition(int sceneNum)
    {
        
        transition.Play("New Animation");
        
        
        yield return new WaitForSeconds(duration);
        SceneManager.LoadSceneAsync(sceneNum);
        


    }

    private void SetTransInactive()
    {
        transitionIn.SetActive(false);
        transitionOut.SetActive(false);
    }
    private  IEnumerator setTransitionInactive()
    {
        
        yield return new WaitForSeconds(0.5f);
        Debug.Log("asdsadasd");
        SetTransInactive();



    }

    public void OnEnable()
    {
        transitionIn.SetActive(true);
        transitionOut.SetActive(true);
        StartCoroutine(setTransitionInactive());
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
