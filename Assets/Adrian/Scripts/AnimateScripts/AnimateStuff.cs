using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateStuff : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> animations;

    [SerializeField]
    private AudioClip openSounds;

    [SerializeField]
    private AudioClip closeSounds;

    [SerializeField]
    private bool open = false;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

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

    public void DoTheAnimation(int animationIndex, string openAnimation, string closeAnimation)
    {
        Animator animator = animations[animationIndex].GetComponent<Animator>();
        Interactable interactable = animations[animationIndex].GetComponent<Interactable>();

        if (open)
        {
            interactable.hoverMsg = "Open [E]";
            animator.Play(closeAnimation);
            PlaySound(openSounds);
            open = false;
        }
        else
        {
            interactable.hoverMsg = "Close [E]";
            animator.Play(openAnimation);
            PlaySound(closeSounds);
            open = true;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
