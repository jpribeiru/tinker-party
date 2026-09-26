using UnityEngine;
using TMPro;

public class P1Scoreboard : MonoBehaviour
{
    public Player1 player1;

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = "Hits: " + Player1.player1Hits;
    }
}
