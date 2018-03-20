using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTest : MonoBehaviour {

    public Text text;
    public Text optionA;
    public Text optionB;
    public Text nameWindow;
    public GameObject A;
    public GameObject B;
    public Canvas canvas;
    public Animator anim;

    Interaction interaction;

    private bool optionB1 = false;
    private bool optionA1 = false;
    private bool optionAA = false;
    private bool optionAB = false;
    private bool optionBA = false;
    private bool optionBB = false;
    private bool playerTalking = false;
    private bool currentlyTalking = false;
    private int dialogProgress;

    // Use this for initialization
    void Start () {
        dialogProgress = 0;
        canvas.enabled = false;

        anim = GetComponent<Animator>();
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
            if (Input.GetKeyDown(KeyCode.Space) && currentlyTalking == false)
            {
                anim.SetBool("talking", true);
                canvas.enabled = true;
                optionA1 = false;
                optionB1 = false;
                optionAA = false;
                optionAB = false;
                optionBA = false;
                optionBB = false;
                A.SetActive(true);
                B.SetActive(true);
                dialogProgress = 0;
                currentlyTalking = true;
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
            nameWindow.text = interaction.npcName;
            text.text = interaction.textInitiate;
            optionA.text = interaction.textButtonA;
            optionB.text = interaction.textButtonB;
        }

        if(dialogProgress == 1)
        {
            if (optionA1)
            {
                if (playerTalking)
                {
                    nameWindow.text = "You: ";
                    text.text = interaction.textA;
                    A.SetActive(false);
                    B.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        playerTalking = false;
                    }
                }
                else
                {
                    nameWindow.text = interaction.npcName;
                    text.text = interaction.textChoseA;
                    optionA.text = interaction.textButtonAA;
                    optionB.text = interaction.textButtonAB;
                    A.SetActive(true);
                    B.SetActive(true);
                }
            }

            if (optionB1)
            {
                if (playerTalking)
                {
                    nameWindow.text = "You: ";
                    text.text = interaction.textB;
                    A.SetActive(false);
                    B.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        playerTalking = false;
                    }
                }
                else
                {
                    nameWindow.text = interaction.npcName;
                    text.text = interaction.textChoseB;
                    optionA.text = interaction.textButtonBA;
                    optionB.text = interaction.textButtonBB;
                    A.SetActive(true);
                    B.SetActive(true);
                }
            }
        }

        if(dialogProgress == 2)
        {
            if (optionAA)
            {
                if (playerTalking)
                {
                    nameWindow.text = "You: ";
                    text.text = interaction.textAA;
                    A.SetActive(false);
                    B.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        playerTalking = false;
                    }
                }
                else
                {
                    nameWindow.text = interaction.npcName;
                    A.SetActive(true);
                    B.SetActive(true);
                    if (interaction.correctOrder)
                    {
                        Convinced();
                    }
                    else
                    {
                        text.text = interaction.textChoseAA;
                        FinishDialog();
                    }
                }
 
            }

            if (optionAB)
            {
                if (playerTalking)
                {
                    nameWindow.text = "You: ";
                    text.text = interaction.textAB;
                    A.SetActive(false);
                    B.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        playerTalking = false;
                    }
                }
                else
                {
                    nameWindow.text = interaction.npcName;
                    A.SetActive(true);
                    B.SetActive(true);
                    if (interaction.correctOrder)
                    {
                        Convinced();
                    }
                    else
                    {
                        text.text = interaction.textChoseAB;
                        FinishDialog();
                    }
                }

            }

            if (optionBA)
            {
                if (playerTalking)
                {
                    nameWindow.text = "You: ";
                    text.text = interaction.textBA;
                    A.SetActive(false);
                    B.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        playerTalking = false;
                    }
                }
                else
                {
                    nameWindow.text = interaction.npcName;
                    A.SetActive(true);
                    B.SetActive(true);
                    if (interaction.correctOrder)
                    {
                        Convinced();
                    }
                    else
                    {
                        text.text = interaction.textChoseBA;
                        FinishDialog();
                    }
                }

            }

            if (optionBB)
            {
                if (playerTalking)
                {
                    nameWindow.text = "You: ";
                    text.text = interaction.textBB;
                    A.SetActive(false);
                    B.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        playerTalking = false;
                    }
                }
                else
                {
                    nameWindow.text = interaction.npcName;
                    A.SetActive(true);
                    B.SetActive(true);
                    if (interaction.correctOrder)
                    {
                        Convinced();
                    }
                    else
                    {
                        text.text = interaction.textChoseBB;
                        FinishDialog();
                    }
                }
            }
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

    #region ButtonBehaviour
    public void CloseDialog()
    {
        canvas.enabled = false;
        optionA1 = false;
        optionB1 = false;
        optionAA = false;
        optionAB = false;
        optionBA = false;
        optionBB = false;
        interaction.correctOrder = false;
        playerTalking = false;
        dialogProgress = 0;
        currentlyTalking = false;
        anim.SetBool("talking", false);
    }

    public void ChoseOptionA()
    {
        if (dialogProgress == 0)
        {
            optionA1 = true;
            playerTalking = true;
            dialogProgress = 1;
            return;
        }

        if (dialogProgress == 1 && optionA1)
        {
            optionAA = true;
            playerTalking = true;
            dialogProgress = 2;
        }
        else if (dialogProgress == 1 && optionB1)
        {
            optionBA = true;
            playerTalking = true;
            dialogProgress = 2;
        }

    }

    public void ChoseOptionB()
    {
        if (dialogProgress == 0)
        {
            optionB1 = true;
            playerTalking = true;
            dialogProgress = 1;
            return;
        }

        if (dialogProgress == 1 && optionB1)
        {
            optionBB = true;
            playerTalking = true;
            dialogProgress = 2;
        }
        else if(dialogProgress == 1 && optionA1)
        {
            optionAB = true;
            playerTalking = true;
            dialogProgress = 2;
        }
    }
    #endregion

    bool CorrectOrder()
    {
        if(interaction.correctAA)
        {
            if (optionAA)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (interaction.correctAB)
        {
            if (optionAB)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (interaction.correctBA)
        {
            if (optionBA)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (interaction.correctBB)
        {
            if (optionBB)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    void Convinced()
    {
        text.text = interaction.textConvinced;
        interaction.convinced = true;
        FinishDialog();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CloseDialog();
        }
    }
}
