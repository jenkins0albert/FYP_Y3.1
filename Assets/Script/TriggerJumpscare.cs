using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJumpscare : MonoBehaviour
{
    public GameObject playerCamera;       // Reference to the player's camera
    public Camera jumpscareCamera;    // Reference to the jumpscare camera
    public float jumpscareDuration = 2f;  // Duration of the jumpscare
    public AudioSource jumpscareAudio;

    [SerializeField]
    public GameManager gameManager;

    [SerializeField]
    private PlayerInteraction player;

    public void Start()
    {
        
        player = FindAnyObjectByType<PlayerInteraction>();
        playerCamera = GameObject.Find("NestedCamera");
        gameManager = FindObjectOfType<GameManager>();
    }
    public void Awake()
    {

        //gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Jumpscare");
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
        Debug.Log("Jumpscare");
        jumpscareCamera.gameObject.SetActive(true);
        // Disable the player's camera
        playerCamera.SetActive(false);
        gameManager.CanvasInactive();
        // Enable the jumpscare camera
        

        StartCoroutine(EndJumpscare());
    }

    System.Collections.IEnumerator EndJumpscare()
    {
        // Wait for the duration of the jumpscare
        yield return new WaitForSeconds(jumpscareDuration);

        gameManager.GameOver();
    }
}

