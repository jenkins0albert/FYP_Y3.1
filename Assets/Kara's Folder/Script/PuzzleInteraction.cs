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

    [SerializeField]
    private GameObject colliders; // Prevents the puzzle pieces from falling out of the area

    [SerializeField]
    private GameObject puzzlePlaceholder;

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

        // Ensure the colliders are initially inactive
        if (colliders != null)
        {
            colliders.SetActive(false);
        }

        // Ensure that the placeholders are initially inactive
        if (puzzlePlaceholder != null)
        {
            puzzlePlaceholder.SetActive(false);
        }

        // Ensure the player interaction is set
        if (player == null)
        {
            player = FindObjectOfType<PlayerInteraction>();
        }
    }

    public void ActivatePuzzle()
    {
        if (!isPuzzleActive)
        {
            // Switch to the puzzle camera
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

            // Activates the colliders
            if (colliders != null)
            {
                colliders.SetActive(true);
            }

            // Activates the Puzzle Placeholder
            if (puzzlePlaceholder != null)
            {
                puzzlePlaceholder.SetActive(true);
            }

            // Disable player interaction
            if (player != null)
            {
                player.gameObject.SetActive(false);
            }

            isPuzzleActive = true;
            if (outline != null)
            {
                outline.enabled = false; // Turns off the outline
            }
        }
    }

    public void DeactivatePuzzle()
    {
        if (isPuzzleActive)
        {
            // Switch back to the player
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

            // Deactivate the collider
            if (colliders != null)
            {
                colliders.SetActive(false);
            }

            // Deactivate the Puzzle Placeholder
            if (puzzlePlaceholder != null)
            {
                puzzlePlaceholder.SetActive(false);
            }

            // Enable player interaction
            if (player != null)
            {
                player.gameObject.SetActive(true);
            }

            isPuzzleActive = false;
            if (outline != null)
            {
                outline.enabled = true; // Turns on the outline if it's not in the Puzzle Area
            }
        }
    }
}
