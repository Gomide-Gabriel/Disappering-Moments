using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportRoom : MonoBehaviour
{
    public Animator sairDaTask;

    public GameObject[] pratos;
    public string[] text;
    int displayNum;

    Animator speach;
    Text uiText;
    float time;
    public float transitionTime;

    public Transform targetRoom;
    public Transform targetCam;

    CamTarget cam;

    bool pressed;
    public bool active;
    public bool task;


    private void Start()
    {
        cam = GameObject.FindObjectOfType<CamTarget>();

        speach = GameObject.Find("Dialogue").GetComponent<Animator>();
        uiText = GameObject.Find("Speach").GetComponentInChildren<Text>();
        time = 0;
        task = false;
        active = false;

        for (int i = 0; i < pratos.Length; i++)
        {
            pratos[i].GetComponent<Animator>().SetBool("active", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cecilia"))
        {
            //teleporta Player
            Transform playerTrans = collision.transform;
            playerTrans.position = targetRoom.position;

         
            //telporta Camera
            cam.target = targetCam;
        }

        if (collision.CompareTag("Jose"))
        {
            // teleporta o jose
            Transform joseTrans = collision.transform;
            joseTrans.position = targetRoom.position;
        }
    }

    public void Teleport(Transform target)
    {
        Movement.canMove = false;
        cam.target = target;
        FindObjectOfType<TasksGerency>().canCount = true;
        task = true;
        if (!active) pressed = true;
        else sairDaTask.SetBool("active", true);
    }

    public void OutTeleport(Transform target)
    {
        Movement.canMove = true;
        
        cam.target = target;

        sairDaTask.SetBool("active", false);
    }

    private void Update()
    {
        if (pressed)
        {
            speach.SetBool("speach", true);

            if (displayNum < text.Length)
            {
                uiText.text = text[displayNum];

                time += Time.deltaTime; ;

                if (time >= transitionTime)
                {
                    displayNum++;
                    time = 0;
                }
            }
            else
            {
                time = 0;
                speach.SetBool("speach", false);
                displayNum = 0;
                FindObjectOfType<AudioManagerOld>().Play("tiktak");
                Jose.joseState = Jose.JoseState.WashingTasks;

                sairDaTask.SetBool("active", true);
                pratos[0].GetComponent<Animator>().SetBool("active", true);
                pressed = false;
                active = true;
            }
        }
    }
}
