using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharTrigger : MonoBehaviour
{
    public CameraShake cameraShake;
    public AudioSource playSound;
    public GameObject globalVol;

    void OnTriggerStay(Collider other)
    {
        StartCoroutine(cameraShake.Shake(.01f));
    }
    void OnTriggerEnter()
    {
        playSound.Play();
        globalVol.SetActive(true);
    }
    void OnTriggerExit()
    {
        playSound.Stop();
        globalVol.SetActive(false);
    }
}

