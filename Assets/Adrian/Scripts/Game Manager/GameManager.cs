using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{


    public GameObject playerPrefab;

    private PlayerInteraction activeplayer;

    public static GameManager Instance;

    public GameObject gameOverScreen;


    
    private void Awake()
    {
       

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);


        }

        else
        {
            DontDestroyOnLoad(gameObject);

            SceneManager.activeSceneChanged += SpawnPlayerOnSceneLoad;


            Instance = this;
        }



    }

    private void SetInactiveOnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Starting Menu")
        {
            // Call your function here

        }
    }

    public void CanvasInactive()
    {
        PlayerInput playerinput = activeplayer.GetComponent<PlayerInput>();
        activeplayer.SetCanvasInactive();
        playerinput.enabled = false;
        
    }
    public void GameOver()
    {
       
        gameOverScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;


    }

    public void RestartScene()
    {
        PlayerInput playerinput = activeplayer.GetComponent<PlayerInput>();
        
        gameOverScreen.SetActive(false);
        playerinput.enabled = true;

        activeplayer.SetNestedCameraActive();  
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        activeplayer.SetCanvasActive();

        GameObject capsuleCollider = GameObject.Find("PlayerHitCapsule");
        CapsuleCollider hitbox = capsuleCollider.GetComponent<CapsuleCollider>();
        hitbox.enabled = true;

    }
    private void InactivePlayerAndManager()
    {
        activeplayer = FindObjectOfType<PlayerInteraction>();
        activeplayer.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    private void SpawnPlayerOnSceneLoad(Scene currentscene, Scene nextscene)
    {

        if (nextscene.name != "Starting Menu")
        {
            activeplayer = FindObjectOfType<PlayerInteraction>();
            
            if (activeplayer == null)
            {
                SpawnPoint spawnpoint = FindObjectOfType<SpawnPoint>();
                GameObject newplayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                Cursor.lockState = CursorLockMode.Locked;

               
                

                activeplayer = newplayer.GetComponent<PlayerInteraction>();

                activeplayer.transform.position = spawnpoint.transform.position;
                activeplayer.transform.rotation = spawnpoint.transform.rotation;
            }

            else
            {
                
                SpawnPoint spawnpoint = FindObjectOfType<SpawnPoint>();

                

                activeplayer.transform.position = spawnpoint.transform.position;
                activeplayer.transform.rotation = spawnpoint.transform.rotation;
            }
        }

        else
        {
            InactivePlayerAndManager();
        }
        


    }


}
