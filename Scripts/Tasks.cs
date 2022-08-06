using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks
{
    //public int tasks;
    public List<string> textForDialog = new List<string>();

    [HideInInspector]
    public List<string> dialogue = new List<string>();


    public string id;
    public string text;
    public string[] choices;



    // Start is called before the first frame update
    void Start()
    {

    }
}
