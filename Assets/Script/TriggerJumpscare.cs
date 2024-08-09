using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJumpscare : MonoBehaviour
{
    public GameObject playerCamera;       // Reference to the player's camera
    public Camera jumpscareCamera;    // Reference to the jumpscare camera
    public float jumpscareDuration = 2f;  // Duration of the jumpscare
    public AudioSource jumpscareAudio;
    public float shakeDuration = 0.5f;    // Duration of the camera shake
    public float shakeMagnitude = 0.5f;   // Magnitude of the camera shake

    [SerializeField]
    public GameManager gameManager;

    [SerializeField]
    private PlayerInteraction player;

    private Vector3 jumpscareOriginalPos;

    public void Start()
    {
        player = FindAnyObjectByType<PlayerInteraction>();
        playerCamera = GameObject.Find("NestedCamera");
        gameManager = FindObjectOfType<GameManager>();

        if (jumpscareCamera != null)
        {
            jumpscareOriginalPos = jumpscareCamera.transform.localPosition;
        }
    }

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
            StartCoroutine(PlayJumpscareAudio());
        }

        jumpscareCamera.gameObject.SetActive(true);
        // Disable the player's camera
        playerCamera.SetActive(false);
        gameManager.CanvasInactive();

        StartCoroutine(CameraShake(shakeDuration, shakeMagnitude));
        StartCoroutine(EndJumpscare());
    }

    IEnumerator CameraShake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            jumpscareCamera.transform.localPosition = jumpscareOriginalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        jumpscareCamera.transform.localPosition = jumpscareOriginalPos;
    }

    IEnumerator PlayJumpscareAudio()
    {
        jumpscareAudio.Play();
        yield return new WaitForSeconds(jumpscareDuration);
        jumpscareAudio = null ;
    }

    IEnumerator EndJumpscare()
    {
        // Wait for the duration of the jumpscare
        yield return new WaitForSeconds(jumpscareDuration);

        gameManager.GameOver();
    }
}
