using UnityEngine;

public class Player2 : MonoBehaviour
{
    public static int player2Hits = 0;

    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player 2 hit an enemy!");
            player2Hits++;
        }
    }

    void LateUpdate()
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            ArduinoConnector.P2zPos
            );
    }
}