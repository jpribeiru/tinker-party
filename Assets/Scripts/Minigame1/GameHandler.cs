using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour
{ // gathers a whole buncbh of game objects tgat ut wukk yse
    public Player1 player1;
    public Player2 player2;
    public GameObject gameEnder;

    public GameObject tie;
    public GameObject p1win;
    public GameObject p2win;

    private float countdownTime = 5f;
    private bool gameStarted = false;
    public static string winners = "undecided";
    void Start()
    {
        gameStarted = true; //doesnt start actual game loop until the menu test is over, explained below
        gameEnder.SetActive(false);

        player1.GetComponent<Player1>().enabled = true; //player cant move until tut over, script disabled
        player2.GetComponent<Player2>().enabled = true;
        GetComponent<PlayableDirector>().enabled = true; //track doesnt start until tut over, script disabled
        
    }
    IEnumerator Delay() // i heard this is a good way to make a delay, so i used it to make the end screen stay for 5 seconds before going back to main menu
    {
        yield return new WaitForSeconds(5f);

        // Code here runs after 5 seconds
        Debug.Log("5 seconds passed!");
    }
    // Update is called once per frame
    void Update()
    {
        if (gameStarted == true){
            if(gameEnder.activeSelf == false){ //prevents game continously restarting
            Debug.Log("Game is running");
            }
            else{
                player1.GetComponent<Player1>().enabled = false; //game stops
                player2.GetComponent<Player2>().enabled = false;
                GetComponent<PlayableDirector>().enabled = false;
                if(Player1.player1Hits < Player2.player2Hits){ // cgeck winners, winners variable is used by score manager to know which player to give a point to
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