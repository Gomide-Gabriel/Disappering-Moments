using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Jose : MonoBehaviour
{
    Rigidbody2D rb;
    float scaleX;

    public float speed;

    // time to random
    public float time;

    // time to states
    public float stateTime;

    // time to change walk trajectory
    public float walkTime;

    public bool walkedLeft;

    /// Walk State
    // time to change trajectory
    float randomMoveTime;

    // time to random
    float timeToRand;
    /// </summary>

    // tasks 
    public Transform taskTrans;
    public float timeTaks;

    //Washing Task
    //tempo pra textos
    float t;
    public float tCap;

    bool nexxt;

    SpriteRenderer sprite;
    int displayNum;
    
    bool washingComplete;

    public GameObject triggerQuest;
    // Fim da WashingTask

    // SweepingTask






    //fim da SweepTask

    public string tutorialText;
    public string[] text;
    Text uiText;
    Animator speach;


    [SerializeField] public static JoseState joseState = JoseState.Idle;
    private float timeTasks;

    public enum JoseState
    {
        Stand,
        Idle,
        Walk,
        Remember,
        WashingTasks,
        SweepingTasks
    }

    // Start is called before the first frame update
    void Start()
    {

        speach = GameObject.Find("Dialogue").GetComponent<Animator>();
        uiText = GameObject.Find("Speach").GetComponentInChildren<Text>();

        sprite = GetComponentInChildren<SpriteRenderer>();
        t = 0;
        nexxt = false;
        displayNum = 0;
        washingComplete = false;

        rb = GetComponent<Rigidbody2D>();
        stateTime = 0;
        walkTime = 0;
        walkedLeft = false;
        scaleX = transform.localScale.x;

        //timeTaks = 0;
        // Troca a Seed
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
    }

    // Update is called once per frame
    void Update()
    {
        switch (joseState)
        {
            case JoseState.Stand:
                rb.velocity = Vector2.zero;
                GetComponentInChildren<Animator>().SetBool("walk", false);
                break;

            case JoseState.Idle:
                Idle();
                GetComponentInChildren<Animator>().SetBool("walk", false);
                break;

            case JoseState.Walk:
                Walk();
                GetComponentInChildren<Animator>().SetBool("walk", true);
                //Vector2 movement = new Vector2(speed, rb.velocity.y);
                //rb.velocity = -movement;

                break;

            case JoseState.Remember:
                Remember();
                break;

            case JoseState.WashingTasks:
                WashingTask();
                GetComponentInChildren<Animator>().SetBool("walk", false);
                //Debug.Log("FOI");
                break;

            case JoseState.SweepingTasks:


                break;
        }
       
    }

   
    void Idle()
    {
      
        rb.velocity = Vector2.zero;
    
        float randomMoveTime = UnityEngine.Random.Range(time *2 , time * 4);
        stateTime += Time.deltaTime;

        // Troca os states
        if (stateTime > randomMoveTime)
        {
            joseState = JoseState.Walk;
            stateTime = 0;
            walkTime = 0;
            // Troca a Seed
            UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        }
        

        // random to walking tbm
        /* float randomWay = UnityEngine.Random.Range(0, 50);
         if (randomWay > 25) walkedLeft = true;
         else walkedLeft = false;
        */
    }

    void Walk()
    {

        //mudança de state
        stateTime += Time.deltaTime;
        float randomStateTime = UnityEngine.Random.Range(time * 2, time * 5);
      

        // movimento pro rigbody
        Vector2 movement = new Vector2(speed, rb.velocity.y);

        // booleanas
        if (!walkedLeft)
        {
            walkTime += Time.deltaTime;
            randomMoveTime = UnityEngine.Random.Range(time * 1, time * 4);
        }
        else 
        {
            walkTime -= Time.deltaTime;
            randomMoveTime = UnityEngine.Random.Range(time * -1, time * -4);
        }

        // direita
        if (walkTime > randomMoveTime)
        {
            transform.localScale = new Vector2(-scaleX, transform.localScale.y);
            rb.velocity = movement;
            walkedLeft = true;
        }
        // esquerda
        else if (walkTime < randomMoveTime)
        {
            transform.localScale = new Vector2(scaleX, transform.localScale.y);
            rb.velocity = -movement;
            walkedLeft = false;
        }


        // state change
        if (stateTime > randomStateTime)
        {
            joseState = JoseState.Idle;
          //  teste = UnityEngine.Random.Range(0, 5);
            stateTime = 0;
            
            walkTime = 0;
            // Troca a Seed
            UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        }
    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
    void Remember()
    {
        rb.velocity = Vector2.zero;

        walkTime = 0;

        // random to walking tbm
        float randomWay = UnityEngine.Random.Range(0, 50);
        if (randomWay > 25) walkedLeft = true;
        else walkedLeft = false;
    }

    void WashingTask()
    {
      
        stateTime = 0;

        transform.position = taskTrans.position;
        //else joseState = JoseState.Idle;

        timeTaks += Time.deltaTime;
         

        if (timeTaks > 100f && timeTaks < 101f+ tCap)
        {
            
            if (timeTaks > 100f && timeTaks < 101f) FindObjectOfType<AudioManagerOld>().Play("livroCaindo");

            t += Time.deltaTime;

            if (!nexxt)
            {
                if (t < tCap)
                {
                    speach.SetBool("speach", true);
                    uiText.text = tutorialText;
                }
                else
                {
                    t = 0;
                    speach.SetBool("speach", false);
                    nexxt = true;
                }
            }
        }
        else if (timeTaks > 100f && timeTaks < 200f)
        {
            if (sprite.isVisible && nexxt)
            {
                if (displayNum < 2)
                {
                    speach.SetBool("speach", true);
                    Movement.canMove = false;
                    FindObjectOfType<Movement>().mx = 0;

                    t += Time.deltaTime;

                    uiText.text = text[displayNum];

                    if (t > tCap)
                    {
                        displayNum++;
                        t = 0;
                    }
                }
                else
                {
                    speach.SetBool("speach", false);
                    Movement.canMove = true;
                    nexxt = false;
                }
            }
        }
        else if (timeTasks > 200 && timeTasks < 270)
        {
            FindObjectOfType<AudioManagerOld>().Play("livroCaindo");

            if (sprite.isVisible)
            {

                if (displayNum < 4)
                {
                    speach.SetBool("speach", true);
                    Movement.canMove = false;
                    FindObjectOfType<Movement>().mx = 0;

                    t += Time.deltaTime;

                    uiText.text = text[displayNum];

                    if (t > tCap)
                    {
                        displayNum++;
                        t = 0;
                    }
                }
                else
                {
                    speach.SetBool("speach", false);
                    Movement.canMove = true;
                }
            }
        }
        else if (timeTasks > 270)
        {

            FindObjectOfType<AudioManagerOld>().Play("livroCaindo");

            if (sprite.isVisible)
            {

                if (displayNum < 6)
                {
                    speach.SetBool("speach", true);
                    Movement.canMove = false;
                    FindObjectOfType<Movement>().mx = 0;

                    t += Time.deltaTime;

                    uiText.text = text[displayNum];

                    if (t > tCap)
                    {
                        displayNum++;
                        t = 0;
                    }
                }
                else
                {

                    speach.SetBool("speach", false);
                    Movement.canMove = true;
                    displayNum = 0;
                    washingComplete = true;
                }
            }
        }

        if (washingComplete)
        {
            string[] complete = new string[2];
            complete[0] = "Cecilia: Ufa ainda graças a deus eu consegui terminar";
            complete[1] = "Cecilia: O josé realmente tende a repetir suas ações, isso me atrapalha";
            complete[2] = "Cecilia: Mas não é culpa dele, só posso ter paciência e continuar cuidando dele";
            
            if (displayNum < complete.Length - 1)
            {
                speach.SetBool("speach", true);
                Movement.canMove = false;
                FindObjectOfType<Movement>().mx = 0;

                t += Time.deltaTime;

                uiText.text = complete[displayNum];

                if (t > tCap)
                {
                    displayNum++;
                    t = 0;
                }
                else
                {
                    speach.SetBool("speach", false);
                    Movement.canMove = true;
                    displayNum = 0;
                    washingComplete = true;
                    joseState = JoseState.Idle;
                    triggerQuest.SetActive(true);

                }
            }
        }
    }

    void SweeepTask()
    {

    }


}
