using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialJose : MonoBehaviour
{
    public string[] textJose;
    public float timeToDialog;

    float time = 0;

    bool triggered;
    Animator speach;

    int dialogNum = 0;

    SpriteRenderer jose;


    private void Start()
    {
        jose = GameObject.Find("Jose").GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jose") && jose.isVisible)
        {
            speach = GameObject.Find("Dialogue").GetComponent<Animator>();

            DialogueToTrigger dlgTrigger = FindObjectOfType<DialogueToTrigger>();

            if (!triggered)
            {
                dlgTrigger.CatchText(textJose[dialogNum]);
                dialogNum++;
                triggered = true;
                timeToDialog = time;

                Movement.canMove = false;
                Jose.joseState = Jose.JoseState.Stand;
            }
        }

    }
    private void Update()
    {
        // Debug.Log("indice = " + dialogNum + " texto = " + text[dialogNum]);

        IsTrigger();
    }

    void IsTrigger()
    {
        if (triggered)
        {
            speach.SetBool("speach", true);
            timeToDialog += Time.deltaTime;

            if (timeToDialog > 5f)
            {
                if (dialogNum == textJose.Length)
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

                    dlgTrigger.CatchText(textJose[dialogNum]);

                    timeToDialog = time;
                    dialogNum++;
                    // Debug.Log("PARO AQUI 22222");
                }
            }
        }
    }

}
