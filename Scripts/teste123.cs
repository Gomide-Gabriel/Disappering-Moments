using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste123 : MonoBehaviour
{
    public List<string> pamonha;

    string[] texto_display = { "Texto A\n", "Texto B\n", "%", "Texto C\n" };
    string[] texto_escolha = { "Texto A da ESCOLHA\n", "%" };
    string[] choices = { "Escolha A\n", "Escolha B\n" };

    float time_reset = 5;
    public float time;

    public int display_num = 0;
    public int choice_num = 0;

    public int type = 0;

    private void Start()
    {
        time = time_reset;
    }


    void Update()
    {
        if (time < 0 && display_num < texto_display.Length){

            //verifica o tipo
            if (type == 0)
            {
                if (texto_display[display_num] == "%") type++;
            }
            else if (texto_escolha[choice_num] == "%") type--;

            switch (type)
            {
                // caso só texto
                case 0:
                    Debug.Log("Dialogo:\n");
                    if (texto_display[display_num] != "%")
                    {
                        Debug.Log(texto_display[display_num]);
                        if (display_num < texto_display.Length - 1) display_num++;
                    }
                    else if (display_num < texto_display.Length - 1) display_num++;

                    break;

                //caso texto com escolha
                case 1:
                    if (texto_escolha[choice_num] != "%")
                    {
                        //texto de exibição da escolha
                        Debug.Log("Exibição:\n");
                        Debug.Log(texto_escolha[choice_num]);

                        //escolhas
                        Debug.Log("Escolhas:\n");
                        Debug.Log(choices[choice_num]);
                        Debug.Log(choices[choice_num + 1]);
                        if (choice_num < texto_escolha.Length - 1) choice_num++;

                    }
                    else if (choice_num < texto_escolha.Length - 1) choice_num++;
                    break;
            }

            time = time_reset;

        }else if (type == 0) time -= Time.deltaTime;
    }
}
