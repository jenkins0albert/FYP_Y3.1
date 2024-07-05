using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInterval : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource; 

    [SerializeField]
    private float interval = 5.0f; 

    private float timer; 

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); 
        }

        audioSource.loop = false; // ensures audio source does not loop
    }

    private void Update()
    {
        // updates the timer
        timer += Time.deltaTime;

        // checks if the timer has reached the interval
        if (timer >= interval)
        {
            PlayAudio();
            timer = 0f; // reset the timer
        }
    }

    private void PlayAudio()
    {
        audioSource.Play();
    }
}
