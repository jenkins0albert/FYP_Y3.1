using UnityEngine;
using UnityEngine.UI;

public class PuzzleGameInteraction : MonoBehaviour
{
    public Camera puzzleCamera;   // Reference to the puzzle camera
    public GameObject backButton; // Reference to the back button UI

    [SerializeField]
    private PlayerInteraction player;

    [SerializeField]
    private Outline outline;

    private bool isPuzzleActive = false;

    void Start()
    {
        // Ensure the puzzle camera is initially inactive
        if (puzzleCamera != null)
        {
            puzzleCamera.gameObject.SetActive(false);
        }

        // Ensure the back button is initially inactive
        if (backButton != null)
        {
            backButton.SetActive(false);
        }

        player = FindObjectOfType<PlayerInteraction>();
    }

    public void ActivatePuzzle()
    {
        if (!isPuzzleActive)
        {
            // Switch to the puzzle camera
            if (player != null)
            {
                player.gameObject.SetActive(false);
            }

            if (puzzleCamera != null)
            {
                puzzleCamera.gameObject.SetActive(true);
            }

            // Show the back button
            if (backButton != null)
            {
                backButton.SetActive(true);
            }

            // Unlock the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            isPuzzleActive = true;
            outline.enabled = false; // Turns off the outline
        }
    }

    public void DeactivatePuzzle()
    {
        if (isPuzzleActive)
        {
            // Switch back to the player
            if (player != null)
            {
                player.gameObject.SetActive(true);
            }

            if (puzzleCamera != null)
            {
                puzzleCamera.gameObject.SetActive(false);
            }

            // Hide the back button
            if (backButton != null)
            {
                backButton.SetActive(false);
            }

            // Lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            isPuzzleActive = false;
            outline.enabled = true; // Turns on the outline
        }
    }
}
