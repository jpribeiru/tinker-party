using UnityEngine;
using TMPro;

public class PortHandler : MonoBehaviour
{
    public GameObject textArea;
    public static string portLoc;

    public void Click(){
        portLoc = textArea.GetComponent<TMP_Text>().text.Replace("\u200B", "").Trim();
        Debug.Log("Set: " + portLoc);
    }
}
