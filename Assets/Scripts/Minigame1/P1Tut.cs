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
        if (ArduinoConnector.P1zPos > 3.9f)
        {
            goalTouch1 = true;
        }
        else
        {
            goalTouch1 = false;
        }
        rect.anchoredPosition = new Vector2(
        (550f / 8f) * ArduinoConnector.P1zPos + 175f,
        rect.anchoredPosition.y
        );

    }
}
