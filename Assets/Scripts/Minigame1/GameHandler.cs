using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour
{
    public Player1 player1;
    public Player2 player2;
    public GameObject tutOverlay;
    public TextMeshProUGUI countdownText;
    public GameObject gameEnder;

    public GameObject tie;
    public GameObject p1win;
    public GameObject p2win;

    private float countdownTime = 5f;
    private bool gameStarted = false;
    public static string winners = "undecided";
    void Start()
    {
        gameStarted = false;
        gameEnder.SetActive(false);

        player1.GetComponent<Player1>().enabled = false;
        player2.GetComponent<Player2>().enabled = false;
        GetComponent<PlayableDirector>().enabled = false;
        tutOverlay.SetActive(true);

    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5f);

        // Code here runs after 5 seconds
        Debug.Log("5 seconds passed!");
    }
    // Update is called once per frame
    void Update()
    {
        if(gameStarted == false){
            

            if (P1Tut.goalTouch1 && P2Tut.goalTouch2)
            { 
                countdownTime -= Time.deltaTime;
                countdownText.text = Mathf.Max(0f, countdownTime).ToString("F1");
                if (countdownTime < 0f)
                {
                    gameStarted = true;
                    tutOverlay.SetActive(false);
                    player1.GetComponent<Player1>().enabled = true;
                    player2.GetComponent<Player2>().enabled = true;
                    GetComponent<PlayableDirector>().enabled = true;
                }
            }
            else
            {
                countdownTime = 5f;
                countdownText.text = "";
            }
        }
        if (gameStarted == true){
            if(gameEnder.activeSelf == false){
            Debug.Log("Game is running");
            }
            else{
                player1.GetComponent<Player1>().enabled = true;
                player2.GetComponent<Player2>().enabled = false;
                GetComponent<PlayableDirector>().enabled = false;
                if(Player1.player1Hits < Player2.player2Hits){
                    p1win.SetActive(true);
                    StartCoroutine(Delay());
                    winners = "P1";
                }
                else if(Player2.player2Hits < Player1.player1Hits)
                {
                    p2win.SetActive(true);
                    StartCoroutine(Delay());
                    winners = "P2";
                }
                else{
                    tie.SetActive(true);
                    StartCoroutine(Delay());
                    winners = "tie";
                }
            }
        }
    }
}