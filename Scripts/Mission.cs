using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{

    

    public void ExecuteDialogue(string id)
    {
        /*Text[] UiText = new Text[3];
        UiText[0] = GameObject.Find("Speach").GetComponent<Text>();
        UiText[1] = GameObject.Find("Choice").GetComponent<Text>();
        UiText[2] = GameObject.Find("Choice2").GetComponent<Text>();

        Animator dialogueContainer = GameObject.Find("Dialogue").GetComponent<Animator>();
        */
        List<Tasks> textToDialogue = new List<Tasks>();

        textToDialogue.Add(NewTask("vsf", "açllloooooo", new string[] { "pqp", " não entendi" }));
        textToDialogue.Add(NewTask("smth", "filma minha cabesss alek", new string[]{ "abc", "para de ri alekz"}));

        foreach (Tasks item in textToDialogue)
        {
            if (id == item.id)
            {
                Debug.Log(item.text);
                //UiText[0].text = item.text;

                foreach (string choices in item.choices)
                {
                    Debug.Log(choices + "\n");
                   /* for (int i = 1; i < UiText.Length; i++)
                    {
                       // UiText[i].text = choices;
                    }*/
                }
                //dialogueContainer.SetBool("active", true);
            }
        }
       
    }

    public Tasks NewTask(string id, string text ,string[] choices)
    {
        Tasks task = new Tasks();
        task.id = id;
        task.text = text;
        task.choices = choices;

        return task;
    }

    /*public void RunDialogue(string textToDIalogue)
    {
        int i; 

        for ( i = 0; i < 10; i++)
        {
            //if (dialogue[0] != null) dialogue.RemoveAt(0);

            dialogue.Add(textToDIalogue);
            textForDialog.Add(textToDIalogue);

            if (i >= 0) break;
        }
        Debug.Log(dialogue[i]);
        dialogue.RemoveAt(i);
        textForDialog.RemoveAt(i);
    }*/
}
