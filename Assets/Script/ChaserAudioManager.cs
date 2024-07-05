using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserAudioManager : MonoBehaviour
{
    public AudioSource idleAudioSource;
    public AudioSource chasingAudioSource;

    public void PlayIdleSound()
    {
        if (!idleAudioSource.isPlaying)
        {
            chasingAudioSource.Stop();
            idleAudioSource.Play();
        }
    }

    public void PlayChasingSound()
    {
        if (!chasingAudioSource.isPlaying)
        {
            idleAudioSource.Stop();
            chasingAudioSource.Play();
        }
    }

    public void StopAllSounds()
    {
        idleAudioSource.Stop();
        chasingAudioSource.Stop();
    }
}


