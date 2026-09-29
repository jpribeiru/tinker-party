using UnityEngine;
using UnityEngine.SceneManagement;

public class GameChooser : MonoBehaviour
{
    public GameObject minigame1Ready;
    public GameObject minigame1NotReady;
    public GameObject minigame2Ready;
    public GameObject minigame2NotReady;
    private string chosenGame = "Minigame1";

    //this script will edit the minigame menu looks to tell which minigame is beign selected. since u choose with the the potentiometer, if p1zpos  is negative (which i use to refer to the readings) that means its too the left, so minigame 1 is selected, and vice versa
    void Update()
    {
        if (ArduinoConnector.P1zPos > 0f)
        {
            chosenGame = "Minigame1"; //turns on green icon to show it s beign slected
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
        if (ArduinoConnector.button1PressedOnce) //loads the scene of game chosebn when button pressed
        {
            SceneManager.LoadScene(chosenGame);
        }
    }
}
