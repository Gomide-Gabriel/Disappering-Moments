using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerQuests : MonoBehaviour
{
    public int Id;
    public Mission2 quest;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jose"))
        {
            if (Id == quest.ID)
            {
                quest.ExecuteDialog();
            }
        }
    }
}
