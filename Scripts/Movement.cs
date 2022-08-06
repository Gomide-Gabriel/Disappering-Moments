using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform camPos;

    public float mx;
    public float speed;

    Rigidbody2D rb;

    public static bool canMove;

    Animator anim;
    public static int state;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        state = 0;
    }

    private void Update()
    {
        camPos = Camera.main.transform;

        Touch toque;

        if (canMove)
        {

            if (Input.touchCount > 0)
            {
                toque = Input.GetTouch(0);

                Vector3 tapPos = Camera.main.ScreenToWorldPoint(toque.position);

                if (tapPos.x < camPos.position.x) MoveLeft();
                else MoveRight();

               
            }
            else mx = 0;
        }

        AnimGerency();
        SoundManager();
    }

    public void MoveRight()
    {
        mx = 1;

        Vector2 move = new Vector2(mx * speed, rb.velocity.y);
        rb.velocity = move;
        transform.localScale = new Vector2(-mx, transform.localScale.y);
    }

    public void MoveLeft()
    {
        mx = -1;

        Vector2 move = new Vector2(mx * speed, rb.velocity.y);
        rb.velocity = move;
        transform.localScale = new Vector2(-mx, transform.localScale.y);
    }

    void AnimGerency()
    {
        if (state == 0)
        {
            if (mx != 0)
            {
                anim.SetBool("walk", true);
                anim.SetBool("idle", false);
            }
            else
            {
                anim.SetBool("walk", false);
                anim.SetBool("idle", true);
            }
        }
        else
        {
            if (mx != 0)
            {
                anim.SetBool("sweep", true);
                anim.SetBool("idle", false);
            }
            else
            {
                anim.SetBool("sweep", false);
                anim.SetBool("idle", true);
            }
        }
       
    }

    void SoundManager()
    {

        if (mx != 0 && canMove) FindObjectOfType<AudioManagerOld>().Play("walk");
        else FindObjectOfType<AudioManagerOld>().Pause("walk");
    }
}
