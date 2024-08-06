using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharTrigger : MonoBehaviour
{
    public CameraShake cameraShake;
    public AudioSource playSound;
    public GameObject globalVol;

    void OnTriggerEnter()
    {
        playSound.Play();
        globalVol.SetActive(true);
        StartCoroutine(cameraShake.Shake(.01f));

    }

}

