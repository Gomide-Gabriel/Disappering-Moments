using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    Transform startPos;
    GameObject player;

    Animator anim;


    // Start is called before the first frame update
    void Start()
    {

        anim = GameObject.Find("heet_19Sleep").GetComponent<Animator>();

        startPos = GameObject.Find("StartPos").GetComponent<Transform>();
        player = GameObject.Find("Cecilia");
        player.GetComponentInChildren<SpriteRenderer>().enabled = false;
        player.transform.position = startPos.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("acordando") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
        {
            player.GetComponentInChildren<SpriteRenderer>().enabled = true;
            anim.SetBool("active", true);
            Movement.canMove = true;
        }
    }
}
