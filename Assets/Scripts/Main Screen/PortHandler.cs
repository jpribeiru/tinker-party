using UnityEngine;
using TMPro;

public class PortHandler : MonoBehaviour
{ //this scrupt allows the user to input their port, which is of course different for everyone, the value si then used by arduino connector
    public GameObject textArea;
    public static string portLoc;

    public void Click(){
        portLoc = textArea.GetComponent<TMP_Text>().text.Replace("\u200B", "").Trim();
        Debug.Log("Set: " + portLoc);
    }
}
