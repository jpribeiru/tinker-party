using UnityEngine;

public class Player1 : MonoBehaviour
{
    public static int player1Hits = 0;
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) //logging hits
        {
            Debug.Log("Player 1 hit an enemy!");
            player1Hits++;
        }
    }

    void LateUpdate()
    {
        transform.localPosition = new Vector3( //moving script
            transform.localPosition.x,
            transform.localPosition.y,
            ArduinoConnector.P1zPos
        );
    }
}