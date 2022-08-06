using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksGerency : MonoBehaviour
{
    Animator speach;
    Text uiText;

    public string[] text;
    public float transitionTime;
    float time;
    public int textNum;

    int displayNum;

    public WashingTask[] washingTasks;

    public Transform targetCam;
    CamTarget cam;


    public int count;
    public bool canCount;

    //encerrammento
    Jose jose;
    public Animator sairDaTask;


    // Start is called before the first frame update
    void Start()
    {
        speach = GameObject.Find("Dialogue").GetComponent<Animator>();
        uiText = GameObject.Find("Speach").GetComponentInChildren<Text>();

        jose = FindObjectOfType<Jose>();

        cam = FindObjectOfType<CamTarget>();
        textNum = 0;
        displayNum = 0;
        canCount = false;
    }

    // Update is called once per frame
    void Update()
    {
        NextPrato();
        ManagerText();
        
    }

    public void Teleport(Transform target)
    {
       
        cam.target = target;
        Movement.canMove = true;
    }


    void NextPrato()
    {

        if (displayNum < washingTasks.Length - 1)
        {
            if (washingTasks[displayNum].complete)
            {

                displayNum++;
                if (!washingTasks[displayNum].anim.GetBool("active"))
                {
                    washingTasks[displayNum].anim.SetBool("active", true);
                }
                count++;
            }
        }

        if (canCount)
        {
            if (washingTasks[washingTasks.Length - 1].complete)
            {
                count = washingTasks.Length;
            }
        }
    }


    void ManagerText()
    {
        if (count >= washingTasks.Length)
        {
            if (textNum <= text.Length - 1)
            {

                speach.SetBool("speach", true);

                time += Time.deltaTime;

                uiText.text = text[textNum];

                if (time > transitionTime)
                {

                    textNum++;
                    time = 0;
                }
            }
        }

        if (textNum == text.Length - 1)
        {
            //Debug.Log("ASDISDIOSIVD");
            canCount = false;
            time += Time.deltaTime;

            if (time > transitionTime)
            {
                jose.timeTaks = 0;

                sairDaTask.SetBool("active", false);

                speach.SetBool("speach", false);
                Teleport(targetCam);
                textNum = 0;
                time = 0;
                count = 0;
                Movement.canMove = true;
            }
        }
    }
}
