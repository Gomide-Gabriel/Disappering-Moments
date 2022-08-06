using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class Mission2 : MonoBehaviour
{
    string path = "Assets/Resources/Quest";

    public int ID;

    public int choice;

    public int displayNum;
    public int choiceNum;

    public float time;
    float timeReset = 5;

    public int type;

    public bool pressed;
    public bool started;

    public string[] textDisplay;

    public string[] choices;

    // atribuições glovais
    Animator dialogueContainer;

    public List<Dialogue> arrayDialog;

    Text[] UiText = new Text[3];

    [SerializeField] bool complexDialog = false;

    // Objeto de classe
    //Dialogue dialog = new Dialogue("José: Mas eu não almocei ainda", new string[] { "Resposta A: mas José, você já almoçou, não lembra?", "Resposta B: se bem me lembro você tinha almoçado, tivemos algo especial hoje" }, rotaA, rotaB ) });

    Dialogue dialog;

    Dialogue teste123 = new Dialogue("", new string[] { }, new string[] { }, new string[] { });

    public Dialogue teste;

    private void Start()
    {

        time = -1;
        started = false;

        dialogueContainer = GameObject.Find("Dialogue").GetComponent<Animator>();

        UiText[0] = GameObject.Find("Speach").GetComponentInChildren<Text>();
        UiText[1] = GameObject.Find("ButtonChoice").GetComponentInChildren<Text>();
        UiText[2] = GameObject.Find("ButtonChoice2").GetComponentInChildren<Text>();

        // dialog = new Dialogue("José: Mas eu não almocei ainda", new string[] { "Resposta A: mas José, você já almoçou, não lembra?", "Resposta B: se bem me lembro você tinha almoçado, tivemos algo especial hoje" });
        // dialog2 = new Dialogue("Cecilia : Algo errado?", new string[] { });
        dialog = teste123.StoryJsonReader(path, ID);

        arrayDialog = new List<Dialogue>();
        arrayDialog.Add(dialog);
        //Debug.Log(arrayDialog[1].rotaA[0]);
    
    }
    public void ExecuteDialog()
    {
        started = true;
    }

    public void NextDialog(int answer)
    {
        pressed = true;
        choice = answer;
    }

    public int PrimeiroTake(int ocorrencia, string[] txt)
    {
        int i = 0;
        int count = 0; 
        int index = -1;
        while (i < txt.Length && count < ocorrencia)
        {
            if (txt[i].Contains("%"))
            {
                count++;
                index = i;
            }
            i++;
        }

        return index;
    }

    public string searchChoiceQuote(string[] rota, int choiceAmount)
    {
        int count = 0;
        foreach (string txt in rota)
        {
            if (txt.Length > 0) if (txt[0] == '%') count++;

            Debug.Log("txt: " + txt + "\ncount: " + count.ToString());
            if (count == choiceAmount)
            {
                return txt.Remove(0, 1);
               
            }
        }

        return "";
    }

    public string[] SanitizeNewDialog(string txt)
    {
        int values = 0;

        ///[tipo,rota,texto]

        string[] sanitizedString = new string[2] { " ", " " };
        if (txt.Length > 1 && (txt[0] == '%' || txt[0] == '&'))
        {
            //Debug.Log("text primeira posição: " + txt[1] + "text segunda posição: " + txt[2]);
            values++;

            if (txt[1] == ':')
            {
                values++;

                if (txt[2] == 'A' || txt[2] == 'B')
                {
                    values++;
                    sanitizedString[0] = txt[2].ToString();
                }
            }
            else sanitizedString[0] = "DEFAULT TXT";

        }

        if (values > 1) for (int i = 0; i < values; i++) txt = txt.Remove(0, 1);

        sanitizedString[1] = txt;

        return sanitizedString;
    }

    public void ComplexChoices(string[] rotaA, string[] rotaB, int postPosition, string[] choices)
    {
        Dialogue obj;
        List<string[]> newRoutesA = new List<string[]>();
        List<string[]> newRoutesB = new List<string[]>();

        List<string[]> newChoices = new List<string[]>();

        int routeCount = 0;
       

        foreach (string str in rotaA) newRoutesA.Add(SanitizeNewDialog(str));
        foreach (string str in rotaB) newRoutesB.Add(SanitizeNewDialog(str));
        foreach (string str in choices) newChoices.Add(SanitizeNewDialog(str));

        //Eliminando todas combinaçoes de choice + texto de rota
        //for ate o penutimo item de choices (pq anda em par)

        int realMany = 0;
        int skip = 0;

        foreach (string[] item in newChoices)
        {
            if (item[0] == "A" && choice == 0) realMany++;
            if (item[0] == "B" && choice == 1) realMany++;
        }

        foreach (string[] item in newChoices)
        {
            if (choice == 1)
            {
                if (item[0] == "A") skip++;
            }

        }


        //int pqp = 0;

        for (int i = 2 + skip; i < newChoices.Count; i += 2)
        {

            string[] item = new string[2] { "DEFAULT TEXT", "DEFAULT TEXT" };
            string[] par = new string[2] { "DEFAULT TEXT", "DEFAULT TEXT" };
            string[] quote = new string[2] { "DEFAULT TEXT", "DEFAULT TEXT" };

            //quais serão as choices
            Array.Copy(newChoices[i], item, 2);
            string rotaChar = item[0];

            Array.Copy(newChoices[i + 1], par, 2);
            string texto = item[1];

            string[] choiceToAdd = new string[2] { texto, par[1] };

            List<string> respA = new List<string>();
            List<string> respB = new List<string>();
            //string[] respA = new string[];

            //recupera  a quote
            if (rotaChar == "A" && choice == 0)
            {
                string qtxt;

                //pega a quote
                Array.Copy(newRoutesA[routeCount], quote, 2);
                qtxt = quote[1];
                //Ajusta o ponteiro routeCount para a proxima quote de uma choice caso o primeiro item n é uma quote de choice
                while (qtxt[0] != '%' && routeCount < newRoutesA.Count)
                {

                    Array.Copy(newRoutesA[routeCount], quote, 2);
                    qtxt = quote[1];
                    routeCount++;
                }

                //pegando os proximos dialogos (string apenas texto) que vem dps da resposta (apenas texto)
                foreach (string[] var in newRoutesA)
                {
                    if (var[0] == "A") respA.Add(var[1]);
                    else if (var[0] == "B") respB.Add(var[1]);
                }
            }
            else if (rotaChar == "B" && choice == 1)
            {
                string qtxt;

                //pega a quote
                Array.Copy(newRoutesB[routeCount], quote, 2);
                qtxt = quote[1];
                //Ajusta o ponteiro routeCount para a proxima quote de uma choice caso o primeiro item n é uma quote de choice
                while (qtxt[0] != '%' && routeCount < newRoutesB.Count)
                {

                    Array.Copy(newRoutesB[routeCount], quote, 2);
                    qtxt = quote[1];

                    routeCount++;
                }

                //pegando os proximos dialogos (string apenas texto) que vem dps da resposta (apenas texto)
                foreach (string[] var in newRoutesB)
                {
                    if (var[0] == "B") respB.Add(var[1]);
                    else if (var[0] == "A") respA.Add(var[1]);
                }
            }

            string label = quote[1].Remove(0, 1);
            obj = new Dialogue(label.ToString(), choiceToAdd, respA.ToArray(), respB.ToArray());
            arrayDialog.Add(obj);

            break;

        }

        /*/vo mata isso por enquanto dps ele volta
        ///a partir daqui só sobrou texto normal ou nada\
        if (choice == 0)
        {
            //rota A
            for (int i = 0; i < newRoutesA.Count; i++)
            {
                string[] item = newRoutesA[i];
                Array.Copy(item, newRoutesA[i], 2);
                string label = item[1];
                if (item[0] == "A")
                {
                    obj = new Dialogue(txt, new string[] { }, new string[] { }, new string[] { });
                    arrayDialog.Add(obj);
                }
            }

        }
        else
        {
            //rota B
            for (int i = 0; i < newRoutesB.Count; i++)
            { 
                string[] item = newRoutesB[i];
                Array.Copy(item, newRoutesB[i], 2);
                string txt = item[1];
                if (item[0] == "B")
                {
                    obj = new Dialogue(txt, new string[] { }, new string[] { }, new string[] { });
                    arrayDialog.Add(obj);
                }
            }
        }*/

        complexDialog = false;
    }

    public void InsertDialogueByRoute(string[] rota, int postPosition, string[] choices)
    {
        Dialogue obj;


        int choiceCount;
        if (choices.Length > 0) choiceCount = (choices.Length / 2) - 1;
        else choiceCount = 0;

        for (int i = 0; i < rota.Length; i++)
        {
            if (choices.Length == 0)
            {

                obj = new Dialogue(rota[i], new string[] { }, new string[] { }, new string[] { });
                arrayDialog.Add(obj);

                Debug.Log("Sem choices \n Texto: " + rota[i]);
            }
            else
            {

                string[] choiceToAdd = new string[] { };
                if (choices.Length > 2 && choiceCount > 0)
                {
                    for (int j = 2; j < choices.Length; j += 2)
                    {
                        string choiceQuote = searchChoiceQuote(rota, choiceCount);

                        if (choiceQuote != "") choiceToAdd = new string[2] { choices[j], choices[j + 1] };
                        else
                        {
                            choiceToAdd = new string[] { };
                            choiceQuote = rota[i];
                        }
                        obj = new Dialogue(choiceQuote, choiceToAdd, new string[] { }, new string[] { });
                        arrayDialog.Add(obj);
                        choiceCount--;
                    }
                }
                else
                {

                    obj = new Dialogue(rota[i], choiceToAdd, new string[] { }, new string[] { });
                    arrayDialog.Add(obj);
                    Debug.Log("Sem choices\nTexto: " + rota[i]);
                }
            }
        }
    }
    private void Update()
    {
       
        //startou
        if (started)
        {
            // checa se o displayNum é MENOR que a quantidade de items do ArrayDialog, Se passar não estourar o array
            if (displayNum < arrayDialog.Count)
            {
              //  Debug.Log("choice");
                // Checa se o dialogo atual tem Choices
                if (arrayDialog[displayNum].choices.Length > 0)
                {
                    // atribuições padrão
                    UiText[0].text = arrayDialog[displayNum].text;

                    // atribuição das choices
                    UiText[1].text = arrayDialog[displayNum].choices[0];
                    UiText[2].text = arrayDialog[displayNum].choices[1];

                   // Debug.Log("SDVIODSFVODSFV");
                    dialogueContainer.SetBool("active", true);
                    dialogueContainer.SetBool("speach", false);

                    // checa se o NextDialog foi ezecutado, as choices apertadas
                    if (pressed)
                    {
                        // se pressionou passa pro próximo

                        if (arrayDialog[displayNum].rotaA.Length > 0 || arrayDialog[displayNum].rotaB.Length > 0) 
                        {
                            if (!complexDialog)
                            {
                                if (choice == 0) InsertDialogueByRoute(arrayDialog[displayNum].rotaA, displayNum + 1, arrayDialog[displayNum].choices);
                                else InsertDialogueByRoute(arrayDialog[displayNum].rotaB, displayNum + 1, arrayDialog[displayNum].choices);
                            }
                            else
                            {
                                ComplexChoices(arrayDialog[displayNum].rotaA, arrayDialog[displayNum].rotaB, displayNum + 1, arrayDialog[displayNum].choices);
                                //ComplexChoices(arrayDialog[displayNum].rotaB, displayNum + 1, arrayDialog[displayNum].choices);
                            }
                        }
                       
                        displayNum++;
                        pressed = false;
                    }
                }
                // Quando não tiver choices
                else
                {
                    // se der o tempó atriubui as coisa
                    UiText[0].text = arrayDialog[displayNum].text;
                    dialogueContainer.SetBool("active", false);
                    dialogueContainer.SetBool("speach", true);
                   // Debug.Log("to chagando aqui");

                    // checa o timer se for menor que zero
                    if (time > timeReset)
                    {
                        time = 0;
                        displayNum++;
                    }
                    else time += Time.deltaTime;
                }
            }
            // Se for maior esse é o ENCERRAMENTO do bang
            else
            {    
                // Checa se o dialogo atual tem Choices
                if (arrayDialog[displayNum-1].choices.Length > 0)
                {
                   
                    //Debug.Log("fudeu");
                    //Debug.Log("morri aqui "  + displayNum.ToString());

                    dialogueContainer.SetBool("active", false);
                    dialogueContainer.SetBool("speach", false);
                    started = false;
                    pressed = false;
                    arrayDialog.Clear();
                    arrayDialog.Add(dialog);

                    if (ID == 4) complexDialog = true;

                    displayNum = 0;
                    time = 0;
                    
                }
                // Checa se dialogo atual não tem choices
                else
                {
                    // HIT KILL

                    dialogueContainer.SetBool("active", false);
                    dialogueContainer.SetBool("speach", false);
                    started = false;
                    pressed = false;
                    arrayDialog.Clear();
                    arrayDialog.Add(dialog);

                    if (ID == 4) complexDialog = true;

                    time = 0;
                    displayNum = 0;

                }
            }
        }
    }
}
