using UnityEngine;

public class P1Tut : MonoBehaviour
{   private RectTransform rect;
    public static bool goalTouch1 = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        goalTouch1 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ArduinoConnector.P1zPos > 3.9f) // if at the max, that means its touching the goal, not = 4f as small fluctiations can occur, prevents the annoyance from those resetting the timer
        {
            goalTouch1 = true;
        }
        else
        {
            goalTouch1 = false;
        }
        rect.anchoredPosition = new Vector2( //takes the values of p1ZPos, again the readings of the potentiometer, and alows it to shift car icon
        (550f / 8f) * ArduinoConnector.P1zPos + 175f,
        rect.anchoredPosition.y
        );

    }
}
