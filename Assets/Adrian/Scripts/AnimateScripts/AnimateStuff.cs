using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AnimateStuff : MonoBehaviour
{
    
    [SerializeField]
    private List<GameObject> animations;

    [SerializeField]
    private bool open = false;


    public void OpenOven()
    {
        DoTheAnimation(0, "OpenOven", "CloseOven");
    }

    public void OpenDrawer1()
    {
        DoTheAnimation(1, "OpenDrawer1", "CloseDrawer1");
    }

    public void OpenDrawer2()
    {
        DoTheAnimation(2, "OpenDrawer2", "CloseDrawer2");
       
    }

    public void OpenFreezer()
    {
        DoTheAnimation(3, "OpenFreezer", "CloseFreezer");

    }

    public void OpenFridge()
    {
        DoTheAnimation(4, "OpenFridge", "CloseFridge");

    }

    public void DoTheAnimation(int animationcount, string openanimation, string closeanimation)
    {
        if (open == false)
        {
            Animator animator = animations[animationcount].GetComponent<Animator>();
            Interactable interactable = animations[animationcount].GetComponent<Interactable>();
            interactable.hoverMsg = "Close [E]";
            animator.Play(openanimation);
            open = true;
        }

        else
        {
            Animator animator = animations[animationcount].GetComponent<Animator>();
            Interactable interactable = animations[animationcount].GetComponent<Interactable>();
            interactable.hoverMsg = "Open [E]";
            animator.Play(closeanimation);
            open = false;
        }
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
