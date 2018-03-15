using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTest : MonoBehaviour {

    public Text text;
    public Text optionA;
    public Text optionB;
    public GameObject dialogBox;

    private bool optionA1;
    private bool optionA2;
    private bool optionB1;
    private bool optionB2;
    private int dialogProgress;

    // Use this for initialization
    void Start () {
        dialogProgress = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogBox.SetActive(true);
        }
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


    }


    public void CloseDialog()
    {
        dialogBox.SetActive(false);
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
