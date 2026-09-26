using UnityEngine;
using TMPro;

public class M2P1Tut : MonoBehaviour
{
    private float readyTimer = 0f;
    [SerializeField] private float requiredHoldTime = 3f;
    public static bool playersReady = false;
    public GameObject ready1;
    public GameObject notReady1;
    public GameObject ready2;
    public GameObject notReady2;
    public TMP_Text timer;
    void Start()
    {
        ready1.SetActive(false);
        notReady1.SetActive(true);
        ready2.SetActive(false);
        notReady2.SetActive(true);
        timer.text = "";
        playersReady = false;
    }
    private void OnEnable()
    {
        playersReady = false;
    }

    void Update()
    {
        if (ArduinoConnector.button1Pressed || ArduinoConnector.button2Pressed)
        {
            if(ArduinoConnector.button1Pressed){
                ready1.SetActive(true);
                notReady1.SetActive(false);
            }
            else{
                ready1.SetActive(false);
                notReady1.SetActive(true);
            }
            if(ArduinoConnector.button2Pressed){
                ready2.SetActive(true);
                notReady2.SetActive(false);
            }
            else{
                ready2.SetActive(false);
                notReady2.SetActive(true);
            }
            if (ArduinoConnector.button1Pressed && ArduinoConnector.button2Pressed)
            {
                readyTimer += Time.deltaTime;
                timer.text = Mathf.Max(0, requiredHoldTime - readyTimer).ToString("F1");
                if (readyTimer >= requiredHoldTime && !playersReady)
                {
                    playersReady = true;
                    Debug.Log("Both players ready!");
                }
            }
            else
            {
                // Reset timer if either player lets go
                readyTimer = 0f;
                timer.text = "";
            }
        }
        else
        {
            ready1.SetActive(false);
            notReady1.SetActive(true);
            ready2.SetActive(false);
            notReady2.SetActive(true);
            timer.text = "";
        }
    }
}
