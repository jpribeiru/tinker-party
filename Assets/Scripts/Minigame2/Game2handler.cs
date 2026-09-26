using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game2handler : MonoBehaviour
{
    private float Timer = 0f;

    private CubeSpawner[] spawners;
    public static int spawnerIndex;
    private CubeSpawner currentSpawner;
    public Camera mainCamera;

    public GameObject overlay;
    public GameObject P1Win;
    public GameObject P2Win;

    private bool gameRunning = false;

    public static string winner = "undecided";

    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }   

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Game started!");

        overlay.SetActive(true);
        gameRunning = false;

        P1Win.SetActive(false);
        P2Win.SetActive(false);

        winner = "undecided";
        spawnerIndex = 0;
        Timer = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (M2P1Tut.playersReady && !gameRunning)
        {
            overlay.SetActive(false);
            gameRunning = true;
        }
        if (gameRunning == true && 
        ((ArduinoConnector.button1PressedOnce && spawnerIndex == 1) || 
        (ArduinoConnector.button2PressedOnce && spawnerIndex == 0)))
        {
            if (MovingCube.CurrentCube != null)
            {
                MovingCube.CurrentCube.Stop();
            }

            spawnerIndex = spawnerIndex == 0 ? 1 : 0; // 0-X for player 1, 1-Y for player 2
            currentSpawner = spawners[spawnerIndex];

            currentSpawner.SpawnCube();
            mainCamera.transform.position = new Vector3(
                mainCamera.transform.position.x,
                mainCamera.transform.position.y + 0.1f,
                mainCamera.transform.position.z
            );
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5f);

        // Code here runs after 5 seconds
        Debug.Log("5 seconds passed!");
    }
    public void EndGame()
    {
        gameRunning = false;

        if(spawnerIndex == 0)
        {
            Debug.Log("Player 1 Wins!");
            P1Win.SetActive(true);
            StartCoroutine(Delay());
            winner = "1";
        }
        else
        {
            Debug.Log("Player 2 Wins!");
            P2Win.SetActive(true);
            StartCoroutine(Delay());
            winner = "2";
        }

        Debug.Log("Game Over!");
    }
}
