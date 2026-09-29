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

    public static string winner = "undecided"; //sets winner variable to undecided so score manager isnt confused

    private void Awake() // as soon as game starts, finds the cube spawner by finding object tages
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // starts the game by running the tutorial page first before the actual game loop starts
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
    private void Update() // game loop
    {
        if (M2P1Tut.playersReady && !gameRunning) // if both players are ready and the game hasnt started yet, runs the following to start
        {
            overlay.SetActive(false); // hide tutorial and run game loop
            gameRunning = true;
        }
        if (gameRunning == true && 
        ((ArduinoConnector.button1PressedOnce && spawnerIndex == 1) || 
        (ArduinoConnector.button2PressedOnce && spawnerIndex == 0))) // if gameloop started and the player presses their button in their respective turn (aka to cut the bloc)
        {
            if (MovingCube.CurrentCube != null) // stops the cube
            {
                MovingCube.CurrentCube.Stop();
            }

            spawnerIndex = spawnerIndex == 0 ? 1 : 0; // 0-X for player 1, 1-Y for player 2, swapping the turn
            currentSpawner = spawners[spawnerIndex];

            currentSpawner.SpawnCube(); //spawns a new cube
            mainCamera.transform.position = new Vector3( // pushes camera up by height of block (0.1f)
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
        gameRunning = false; // stop game loop, let scoremanager find the winner

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
