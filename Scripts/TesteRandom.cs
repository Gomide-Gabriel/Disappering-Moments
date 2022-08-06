using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TesteRandom : MonoBehaviour
{
    private float[] noiseValues;
    void Start()
    {
        // System.Random rand = new System.Random((int)DateTime.Now.Ticks);
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        noiseValues = new float[10];
        for (int i = 0; i < noiseValues.Length; i++)
        {
            noiseValues[i] = UnityEngine.Random.value;
            Debug.Log(noiseValues[i]);
        }
    }
}
