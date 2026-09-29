using UnityEngine;

public class P2Tut : MonoBehaviour
{   private RectTransform rect;
    public static bool goalTouch2 = false;
    

    //works same as p1tut, read comments there
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        goalTouch2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ArduinoConnector.P2zPos > 3.9f)
        {
            goalTouch2 = true;
        }
        else
        {
            goalTouch2 = false;
        }
        rect.anchoredPosition = new Vector2(
        (550f / 8f) * ArduinoConnector.P2zPos + 175f,
        rect.anchoredPosition.y
        );

    }
}
