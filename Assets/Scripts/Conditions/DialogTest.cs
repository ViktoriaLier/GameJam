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

    private void OnTriggerEnter(Collider other)
    {
        interaction = other.GetComponent<Interaction>();
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
    }

    private void OnTriggerExit(Collider other)
    {
        CloseDialog();
    }

    // Use this for initialization
    void Start () {
        dialogProgress = 0;
        canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
     
    }


    void DialogProgression()
    {
        if(dialogProgress == 0)
        {
            text.text = "That's an example text!";
            optionA.text = "OptionA";
            optionB.text = "OptionB";
        }

        if(dialogProgress == 1 && optionA1)
        {
            text.text = "You chose OptionA! I am still not convinced yet.";
            optionA.text = "OptionA2";
            optionB.text = "OptionB2";
        }

        if (dialogProgress == 1 && optionB1)
        {
            text.text = "You chose OptionB! I am still not convinced yet.";
            optionA.text = "OptionA2";
            optionB.text = "OptionB2";
        }

        if (CorrectOrder() && dialogProgress == 2)
        {
            text.text = "You convinced me!";
            interaction.convinced = true;
            FinishDialog();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CloseDialog();
            }
        }
        else if(!CorrectOrder() && dialogProgress == 2 && optionA2)
        {
            text.text = "You chose OptionA2! I dont think i can trust you.";
            FinishDialog();


        }
        else if (!CorrectOrder() && dialogProgress == 2 && optionB2)
        {
            text.text = "You chose OptionB again! I dont think i can trust you.";
            FinishDialog();

        }
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
