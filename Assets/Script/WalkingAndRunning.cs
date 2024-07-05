using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAndRunning : MonoBehaviour
{
    private AudioSource playerAudioSource;

    [SerializeField]
    private AudioClip walkClip;

    [SerializeField]
    private AudioClip runClip;

    private float walkLoopInterval = 0.8f;  // interval for walking loop
    private float runLoopInterval = 0.3f;   // interval for running loop

    private float loopInterval;  // current loop interval

    private float loopTimer;  // timer for managing loop intervals

    private void Awake()
    {
        playerAudioSource = gameObject.AddComponent<AudioSource>();
        playerAudioSource.loop = true;
    }

    public void PlayWalkingSound()
    {
        if (!playerAudioSource.isPlaying || playerAudioSource.clip != walkClip)
        {
            playerAudioSource.clip = walkClip;
            playerAudioSource.Play();
            loopInterval = walkLoopInterval;
        }
    }

    public void PlayRunningSound()
    {
        if (!playerAudioSource.isPlaying || playerAudioSource.clip != runClip)
        {
            playerAudioSource.clip = runClip;
            playerAudioSource.Play();
            loopInterval = runLoopInterval;
        }
    }

    public void StopSounds()
    {
        playerAudioSource.Stop();
        loopTimer = 0f;
        loopInterval = 0f;
    }

    private void Update()
    {
        // Manage loop interval
        if (playerAudioSource.isPlaying)
        {
            loopTimer += Time.deltaTime;
            if (loopTimer >= loopInterval)
            {
                playerAudioSource.Stop();
                playerAudioSource.Play();
                loopTimer = 0f;
            }
        }
    }
}
