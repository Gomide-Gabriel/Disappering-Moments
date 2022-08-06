using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeSweepTask : MonoBehaviour
{
    GameObject button;

    public string[] text;

    Animator speach;
    Text uiText;
    public int displayNum;
    float time;
    public float timeCap;

    bool pressed;

    private void Start()
    {
        speach = GameObject.Find("Dialogue").GetComponent<Animator>();
        uiText = GameObject.Find("Speach").GetComponentInChildren<Text>();
        button = GameObject.Find("ButtonPos");

        displayNum = 0;
        time = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Movement>())
        {
            button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GetComponent<Movement>())
        {
            button.SetActive(false);
        }
    }


    public void Inicializa()
    {
        pressed = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            if (displayNum < text.Length - 1)
            {
                speach.SetBool("speach", true);

                time += Time.deltaTime;

                uiText.text = text[displayNum];

                if (time > timeCap)
                {
                    displayNum++;
                    time = 0;
                }
            }
            else
            {
                speach.SetBool("speach", false);
                displayNum = 0;
                time = 0;

                Movement.state = 1;
                pressed = false;
            }
        }
    }
}
