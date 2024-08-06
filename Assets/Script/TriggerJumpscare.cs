using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJumpscare : MonoBehaviour
{
    public Camera playerCamera;       // Reference to the player's camera
    public Camera jumpscareCamera;    // Reference to the jumpscare camera
    public float jumpscareDuration = 2f;  // Duration of the jumpscare
    public AudioSource jumpscareAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            JumpscareTrigger();
        }
    }

    void JumpscareTrigger()
    {
        // Play the sound
        if (jumpscareAudio != null)
        {
            jumpscareAudio.Play();
        }

        // Disable the player's camera
        playerCamera.gameObject.SetActive(false);

        // Enable the jumpscare camera
        jumpscareCamera.gameObject.SetActive(true);

        StartCoroutine(EndJumpscare());
    }

    System.Collections.IEnumerator EndJumpscare()
    {
        // Wait for the duration of the jumpscare
        yield return new WaitForSeconds(jumpscareDuration);

        // Re-enable the player's camera
        playerCamera.gameObject.SetActive(true);

        // Disable the jumpscare camera
        jumpscareCamera.gameObject.SetActive(false);
    }
}

