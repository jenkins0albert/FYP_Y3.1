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

    private void SpawnPlayerOnSceneLoad(Scene currentscene, Scene nextscene)
    {
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
            activeplayer = FindObjectOfType<PlayerInteraction>();
            SpawnPoint spawnpoint = FindObjectOfType<SpawnPoint>();
            activeplayer.transform.position = spawnpoint.transform.position;
            activeplayer.transform.rotation = spawnpoint.transform.rotation;
        }


    }


}
