using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueToTrigger : MonoBehaviour
{
    Text UiText;

    public void CatchText(string txt)
    {
        UiText = GameObject.Find("Speach").GetComponentInChildren<Text>();  

        UiText.text = txt;
    }
}
