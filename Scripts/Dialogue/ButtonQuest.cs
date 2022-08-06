using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonQuest : MonoBehaviour
{
    public Transform questItem;
    public Vector2 offSet;

    // Update is called once per frame
    void Update()
    {
        Vector2 target = Camera.main.WorldToScreenPoint(questItem.position);
        transform.position = target + offSet;
    }
}
