using UnityEngine;
using TMPro;

public class P1Scoreboard : MonoBehaviour
{
    public Player1 player1;

    // Update is called once per frame
    void Update() //updates p1 scoreboard
    {
        GetComponent<TMP_Text>().text = "Hits: " + Player1.player1Hits;
    }
}
