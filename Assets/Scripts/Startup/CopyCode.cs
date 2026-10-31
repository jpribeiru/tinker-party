using UnityEngine;
using TMPro;

public class CopyCode : MonoBehaviour
{
    [SerializeField] TMP_Text codeText;
    
    public void Copy()
    {
        GUIUtility.systemCopyBuffer = codeText.text;
    }
}
