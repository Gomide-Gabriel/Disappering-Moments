using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Dialogue
{
    public string text;

    public string[] rotaA;
    public string[] rotaB;

    public string[] choices;

    string path = "Assets/Resources/Quest";

    public Dialogue(string text, string[] choices, string[] rotaA, string[] rotaB)
    {
        this.text = text;
        this.choices = choices;
        this.rotaA = rotaA;
        this.rotaB = rotaB;

    }

    public Dialogue StoryJsonReader(string path, int quest)
    {
        path =  path + quest.ToString() + ".json";


        // Le o arquivo com o seu endereço
        StreamReader reader = new StreamReader(path);

        // Le e atribui numa string pra transformar em jason
        string json = reader.ReadToEnd();

        // pega a string e transforma em um objeto de classe
        Dialogue obj = JsonUtility.FromJson<Dialogue>(json);


        return obj;
    }

}
