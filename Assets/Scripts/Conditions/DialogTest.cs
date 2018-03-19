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

    // Use this for initialization
    void Start () {
        dialogProgress = 0;
        canvas.enabled = false;
    }
	
    private void OnTriggerEnter(Collider other)
    {
        interaction = other.GetComponent<Interaction>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (CorrectOrder())
        {
            interaction.correctOrder = true;
        }
        else
        {
            interaction.correctOrder = false;
        }


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

        if (interaction.interactiveObject)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canvas.enabled = true;
                A.SetActive(false);
                B.SetActive(false);
            }
            ObjectProgression();
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
            text.text = interaction.textInitiate;
            optionA.text = interaction.textA;
            optionB.text = interaction.textB;
        }

        if(dialogProgress == 1 && optionA1)
        {
            text.text = interaction.textChoseA;
            optionA.text = interaction.textAA;
            optionB.text = interaction.textBA;
        }

        if (dialogProgress == 1 && optionB1)
        {
            text.text = interaction.textChoseB;
            optionA.text = interaction.textBA;
            optionB.text = interaction.textBB;
        }

        if (interaction.correctOrder && dialogProgress == 2)
        {
            text.text = interaction.textConvinced;
            interaction.convinced = true;
            FinishDialog();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CloseDialog();
            }
        }
        else if(!interaction.correctOrder && dialogProgress == 2 && optionA2)
        {
            text.text = interaction.textChoseA2;
            FinishDialog();


        }
        else if (!interaction.correctOrder && dialogProgress == 2 && optionB2)
        {
            text.text = interaction.textChoseB2;
            FinishDialog();

        }
    }

    void ObjectProgression()
    {
        text.text = interaction.textObject;
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
        optionA1 = false;
        optionA2 = false;
        optionB1 = false;
        optionB2 = false;
        interaction.correctOrder = false;
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
