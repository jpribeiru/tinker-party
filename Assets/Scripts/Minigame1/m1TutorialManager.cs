using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class m1TutorialManager : MonoBehaviour
{
    [SerializeField]public TMP_Text countdownText;
    private float countdownTime = 5f;

    // Update is called once per frame
    void Update()
    {
        if (P1Tut.goalTouch1 && P2Tut.goalTouch2) // aim is that both players have to move their cars onto the green spot to ttest that the potentiometer is working, which means moving their dial to the MAX
            { 
                countdownTime -= Time.deltaTime; // hold for 5 sec
                countdownText.text = Mathf.Max(0f, countdownTime).ToString("F1");
                if (countdownTime < 0f)
                {
                    Debug.Log("Both players ready!"); // starts game once done
                    SceneManager.LoadScene("Minigame1");
                }
            }
            else
            {
                countdownTime = 5f;
                countdownText.text = "";
            }
    }
}
