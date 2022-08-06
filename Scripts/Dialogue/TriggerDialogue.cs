using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public string[] textCeclia;
    public float timeToDialog;
   
    float time = 0;

    bool triggered;
    Animator speach;

    int dialogNum = 0;

    SpriteRenderer jose;

    /* private void Start()
     {
         int counter = 0;
         while (counter < 10)
         {
             //counter++;
             print(counter);
             counter++;
         }
     }*/

    private void Start()
    {
        jose = GameObject.Find("Jose").GetComponentInChildren<SpriteRenderer>();
        //Debug.Log("AAAAA");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cecilia") && jose.isVisible)
        {

    
            speach = GameObject.Find("Dialogue").GetComponent<Animator>();

            DialogueToTrigger dlgTrigger = FindObjectOfType<DialogueToTrigger>();

            if (!triggered)
            {
                Movement.canMove = false;
                Jose.joseState = Jose.JoseState.Stand;

                dlgTrigger.CatchText(textCeclia[dialogNum]);
                dialogNum++;
                triggered = true;
                timeToDialog = time;
            }
        }
    }

    private void Update()
    {
       // Debug.Log("indice = " + dialogNum + " texto = " + text[dialogNum]);

        IsTrigger();
        Debug.Log("AAAAA");
    }

    void IsTrigger()
    {
        if (triggered)
        {
            speach.SetBool("speach", true);
            timeToDialog += Time.deltaTime;

            if (timeToDialog > 5f)
            { 
                if (dialogNum == textCeclia.Length)
                {
                    speach.SetBool("speach", false);
                    dialogNum = 0;
                    triggered = false;
                    gameObject.SetActive(false);
                    timeToDialog = time;
                    //Debug.Log("PARO AQUI");

                    Movement.canMove = true;
                    Jose.joseState = Jose.JoseState.Idle;

                }
                else
                {
                    DialogueToTrigger dlgTrigger = FindObjectOfType<DialogueToTrigger>();

                    dlgTrigger.CatchText(textCeclia[dialogNum]);
                  
                    timeToDialog = time;
                    dialogNum++;
                   // Debug.Log("PARO AQUI 22222");
                }
            }
        }
    }

    /*void dialogueText(string[] txt)
    {
        txt = text;

        for (int i = 0; i < txt.Length; i++)
        {
            float timeNext = 0;
            timeNext += (int)Time.deltaTime;

            if (timeNext > 2f)
            {
                
            }
        }
    }*/
}
