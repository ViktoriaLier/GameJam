using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTest : MonoBehaviour {

    public Text text;
    public Text optionA;
    public Text optionB;
    public GameObject A;
    public GameObject B;
    public Canvas canvas;

    Interaction interaction;

    private bool optionA1 = false;
    private bool optionA2 = false;
    private bool optionB1 = false;
    private bool optionB2 = false;
    private int dialogProgress;
    private bool objectInteraction = false;

    //TextBox Texts
    private string textInitiate; //Also used for normal interaction!
    private string textChoseA;
    private string textChoseB;
    private string textConvinced;
    private string textChoseA2;
    private string textChoseB2;
    //Wrong Answers
    private string textA3;
    private string textB3;

    // A/B Answers
    private string textA;
    private string textB;
    //Route A
    private string textAA;
    private string textAB;
    //Route B
    private string textBA;
    private string textBB;




    // Use this for initialization
    void Start () {
        dialogProgress = 0;
        canvas.enabled = false;

        //TextBox Texts for Dialog
        textInitiate = "That's an example text!";
        textChoseA = "You chose OptionA! I am still not convinced yet.";
        textChoseB = "You chose OptionB! I am still not convinced yet.";
        textChoseA2 = "You chose OptionA2! I dont think i can trust you.";
        textChoseB2 = "You chose OptionB again! I dont think i can trust you.";
        textConvinced = "You convinced me!";
        textA3 = "You chose OptionA2! I dont think i can trust you.";
        textB3 = "You chose OptionB again! I dont think i can trust you.";

        //Possible Answers
        textA = "OptionA";
        textAA = "OptionAA";
        textAB = "OptionAB";
        textB = "OptionB";
        textBB = "OptionBB";
        textBA = "OptionBA";
    }
	
    private void OnTriggerEnter(Collider other)
    {
        interaction = other.GetComponent<Interaction>();

        if(other.gameObject.tag == "Object")
        {
            objectInteraction = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (interaction.talk)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canvas.enabled = true;
                optionA1 = false;
                optionA2 = false;
                optionB1 = false;
                optionB2 = false;
                A.SetActive(true);
                B.SetActive(true);
                dialogProgress = 0;
            }

            DialogProgression();
        }
        else if (interaction == null)
        {
            return;
        }

        if (objectInteraction)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canvas.enabled = true;
                A.SetActive(false);
                B.SetActive(false);
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        CloseDialog();
    }

	// Update is called once per frame
	void Update () {
     
    }


    void DialogProgression()
    {
        if(dialogProgress == 0)
        {
            text.text = textInitiate;
            optionA.text = textA;
            optionB.text = textB;
        }

        if(dialogProgress == 1 && optionA1)
        {
            text.text = textChoseA;
            optionA.text = textAA;
            optionB.text = textBA;
        }

        if (dialogProgress == 1 && optionB1)
        {
            text.text = textChoseB;
            optionA.text = textBA;
            optionB.text = textBB;
        }

        if (CorrectOrder() && dialogProgress == 2)
        {
            text.text = textConvinced;
            interaction.convinced = true;
            FinishDialog();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CloseDialog();
            }
        }
        else if(!CorrectOrder() && dialogProgress == 2 && optionA2)
        {
            text.text = textA3;
            FinishDialog();


        }
        else if (!CorrectOrder() && dialogProgress == 2 && optionB2)
        {
            text.text = textB3;
            FinishDialog();

        }
    }

    void ObjectProgression()
    {
        text.text = "";
    }

    void FinishDialog()
    {
        A.SetActive(false);
        B.SetActive(false);
    }

#region ButtonScripts
    public void CloseDialog()
    {
        canvas.enabled = false;
    }

    public void ChoseOptionA()
    {
        if (dialogProgress == 0)
        {
            optionA1 = true;
            dialogProgress = 1;
            return;
        }

        if (dialogProgress == 1)
        {
            optionA2 = true;
            dialogProgress = 2;
        }
    }

    public void ChoseOptionB()
    {
        if (dialogProgress == 0)
        {
            optionB1 = true;
            dialogProgress = 1;
            return;
        }

        if (dialogProgress == 1)
        {
            optionB2 = true;
            dialogProgress = 2;
        }
    }
    #endregion

    bool CorrectOrder()
    {
        if(optionA1 && optionB2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
