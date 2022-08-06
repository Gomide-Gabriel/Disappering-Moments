using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingTask : MonoBehaviour
{
    public bool complete;

    public LayerMask layerMask;
    public GameObject[] sujeiras;
    
    public SpriteRenderer rend;
    public SpriteRenderer[] rends;

    RaycastHit2D hit;

    [HideInInspector] public Animator anim;

    float alphaColor;

    public int count;

    public GameObject bolha;


    // Start is called before the first frame update
    void Start()
    {
        complete = false;
        count = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (anim.GetBool("active"))
        {
            Touch toque;

            if (Input.touchCount > 0)
            {
                toque = Input.GetTouch(0);

                Vector3 pos = Camera.main.ScreenToWorldPoint(toque.position);
                pos.z = 0;

                hit = Physics2D.Raycast(pos, Vector3.forward, 10f, layerMask);

                FindObjectOfType<AudioManagerOld>().Play("louça");

                if (hit)
                {
                    bolha.SetActive(true);
                    bolha.transform.position = pos;

                    rend = hit.transform.gameObject.GetComponent<SpriteRenderer>();
                    rend.color = new Vector4(rend.color.r, rend.color.g, rend.color.b, alphaColor);
                    if (alphaColor > 0) alphaColor -= 0.001f * Time.deltaTime;
                    else
                    {
                        hit.transform.gameObject.SetActive(false);
                        count++;
                    }
                }
            }
            else bolha.SetActive(false);
        }

        if (count >= rends.Length)
        {
            complete = true;
            anim.SetBool("active", false);
        }
       /* else
        {
            for (int i = 0; i < rends.Length; i++)
            {
                if (rends[i].color.a <= 0)
                {
                    count++;
                }
            }
        }*/
    }
}
