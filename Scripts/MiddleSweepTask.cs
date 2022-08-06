using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiddleSweepTask : MonoBehaviour
{

    public string[] text;

    Animator speach;
    Text uiText;

    public int displayNum;
    float time;
    public float timeCap;


    private void Start()
    {
        speach = GameObject.Find("Dialogue").GetComponent<Animator>();
        uiText = GameObject.Find("Speach").GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Movement.state == 1 && Globals.sweepCount == 9)
        {
            speach.SetBool("speach", true);

            if (displayNum < text.Length - 1)
            {

            }
        }
    }
}
