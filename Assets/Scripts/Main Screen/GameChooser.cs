using UnityEngine;
using UnityEngine.SceneManagement;

public class GameChooser : MonoBehaviour
{
    public GameObject minigame1Ready;
    public GameObject minigame1NotReady;
    public GameObject minigame2Ready;
    public GameObject minigame2NotReady;
    private string chosenGame = "Minigame1";

    // Update is called once per frame
    void Update()
    {
        if (ArduinoConnector.P1zPos > 0f)
        {
            chosenGame = "Minigame1";
            minigame1Ready.SetActive(true);
            minigame1NotReady.SetActive(false);
            minigame2Ready.SetActive(false);
            minigame2NotReady.SetActive(true);
        }
        else if (ArduinoConnector.P1zPos < 0f)
        {
            chosenGame = "Minigame2";
            minigame1Ready.SetActive(false);
            minigame1NotReady.SetActive(true);
            minigame2Ready.SetActive(true);
            minigame2NotReady.SetActive(false);
        }
        if (ArduinoConnector.button1PressedOnce)
        {
            SceneManager.LoadScene(chosenGame);
        }
    }
}
