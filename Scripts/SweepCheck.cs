using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Movement>())
        {
            Globals.sweepCount++;
        }
    }
}
