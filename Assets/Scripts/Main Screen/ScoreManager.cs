using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private int player1Score = 0;
    [SerializeField] private int player2Score = 0;

    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

    private bool scoreAdded = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu")
        {
            player1ScoreText = GameObject.Find("P1Score").GetComponent<TMP_Text>();
            player2ScoreText = GameObject.Find("P2Score").GetComponent<TMP_Text>();

            UpdateScoreText();
        }

        if (scene.name == "Minigame2")
        {
            scoreAdded = false;
        }
        if (scene.name == "Minigame1")
        {
            scoreAdded = false;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            UpdateScoreText();
        }

        if (SceneManager.GetActiveScene().name == "Minigame1" && !scoreAdded)
        {
            if (GameHandler.winners == "P1")
            {
                player1Score++;
                scoreAdded = true;
                SceneManager.LoadScene("Main Menu");
            }
            else if (GameHandler.winners == "P2")
            {
                player2Score++;
                scoreAdded = true;
                SceneManager.LoadScene("Main Menu");
            }
            else if(GameHandler.winners == "tie"){
                SceneManager.LoadScene("Main Menu");
            }
        }
        if (SceneManager.GetActiveScene().name == "Minigame2" && !scoreAdded)
        {
            if (Game2handler.winner == "1")
            {
                player1Score++;
                scoreAdded = true;
                SceneManager.LoadScene("Main Menu");
            }
            else if (Game2handler.winner == "2")
            {
                player2Score++;
                scoreAdded = true;
                SceneManager.LoadScene("Main Menu");
            }
        }
    }

    void UpdateScoreText()
    {
        if (player1ScoreText != null && player2ScoreText != null)
        {
            Debug.Log("Updating score text: P1: " + player1Score + ", P2: " + player2Score);

            player1ScoreText.text = player1Score.ToString();
            player2ScoreText.text = player2Score.ToString();
        }
    }
}