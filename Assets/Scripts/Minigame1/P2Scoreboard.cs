using UnityEngine;
using TMPro;

public class P2Scoreboard : MonoBehaviour
{
    public Player2 player2;

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = "Hits: " + Player2.player2Hits;
    }
}
