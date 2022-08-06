using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTarget : MonoBehaviour
{

    [SerializeField]
    public Transform target;

    float initialZ;

    public Vector3 pos;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        initialZ = transform.position.z;
        cam = GetComponent<Camera>();
        cam.orthographicSize = 2.051904f;
    }

    // Update is called once per frame
    void Update()
    {

        pos = new Vector3(target.position.x, target.position.y, initialZ);
        transform.position = pos;

    }
}
