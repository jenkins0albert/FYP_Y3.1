using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public GameObject playerPrefab;

    private PlayerInteraction activeplayer;

    public static GameManager Instance;
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
