using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class M2P1Tut : MonoBehaviour
{ //script for the tutorial before minigame 2 starts
    private float readyTimer = 0f;
    [SerializeField] private float requiredHoldTime = 3f;
    public static bool playersReady = false;
    public GameObject ready1;
    public GameObject notReady1;
    public GameObject ready2;
    public GameObject notReady2;
    public TMP_Text timer;
    [SerializeField] string timerPlacText = "Hold for 3 seconds to start!";
    [SerializeField] string sceneToGoTo;
    void Start()
    {
        ready1.SetActive(false);
        notReady1.SetActive(true);
        ready2.SetActive(false);
        notReady2.SetActive(true);
        timer.text = timerPlacText;
        playersReady = false;
    }
    private void OnEnable()
    {
        playersReady = false;
    }

    void Update()
    {
        if (ArduinoConnector.button1Pressed || ArduinoConnector.button2Pressed) //if either presses their button
        {
            if(ArduinoConnector.button1Pressed){ // goes green to show theyve pressed
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
            if (ArduinoConnector.button1Pressed && ArduinoConnector.button2Pressed) // if both holdign at the same time
            {
                readyTimer += Time.deltaTime; // wait 5 seconds for them to be both holding
                timer.text = Mathf.Max(0, requiredHoldTime - readyTimer).ToString("F1");
                if (readyTimer >= requiredHoldTime && !playersReady)
                {
                    playersReady = true;
                    Debug.Log("Both players ready!"); // starts game once done
                    SceneManager.LoadScene(sceneToGoTo);
                }
            }
            else
            {
                // Reset timer if either player lets go
                readyTimer = 0f;
                timer.text = timerPlacText;
            }
        }
        else
        {
            ready1.SetActive(false); // stops tutorial
            notReady1.SetActive(true);
            ready2.SetActive(false);
            notReady2.SetActive(true);
            timer.text = timerPlacText;
        }
    }
}
