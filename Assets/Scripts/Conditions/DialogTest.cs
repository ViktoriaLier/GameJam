using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogTest : MonoBehaviour {

    public Text text;
    public Text optionA;
    public Text optionB;
    public Text nameWindow;
    public GameObject A;
    public GameObject B;
    public Canvas canvas;
    public GameObject panel;
    public Animator anim;
    public Button eButton;
    public Button spaceButton;
    public Button escButton;
    public Text spaceButtonText;

    Interaction interaction;

    private bool optionB1 = false;
    private bool optionA1 = false;
    private bool optionAA = false;
    private bool optionAB = false;
    private bool optionBA = false;
    private bool optionBB = false;
    private bool backstory = false;
    private bool door = false;
    private bool playerTalking = false;
    private bool currentlyTalking = false;
    private int dialogProgress;

    // Use this for initialization
    void Start () {
        dialogProgress = 0;
        canvas.gameObject.SetActive(false);

        anim = GetComponent<Animator>();

    }
	
    private void OnTriggerEnter(Collider other)
    {
        interaction = other.GetComponent<Interaction>();
        canvas.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {

        if (!door)
        {
            if (CorrectOrder())
            {
                interaction.correctOrder = true;
            }
            else
            {
                interaction.correctOrder = false;
            }

            if (interaction.textBackstory != "" && !currentlyTalking)
            {
                eButton.gameObject.SetActive(true);
            }
            else if (interaction.textBackstory == "")
            {
                eButton.gameObject.SetActive(false);
            }

            if (!interaction.interactiveObject && !interaction.convinced)
            {
                spaceButtonText.text = "Talk";
                spaceButton.gameObject.SetActive(true);
            }
            else if (interaction.convinced)
            {
                spaceButton.gameObject.SetActive(false);
            }
            else if (interaction.interactiveObject)
            {
                spaceButtonText.text = "   Interact";
                spaceButton.gameObject.SetActive(true);
            }

            if (panel.activeSelf)
            {
                escButton.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Space) && currentlyTalking == false && !interaction.convinced)
            {
                StartTalking();
            }

            if (dialogProgress > 0)
            {
                spaceButtonText.text = "     Continue";
            }

            if (Input.GetKeyDown(KeyCode.Escape) && escButton.gameObject.activeSelf)
            {
                CloseDialog();
            }

            if (Input.GetKeyDown(KeyCode.E) && eButton.gameObject.activeSelf)
            {
                Backstory();
            }

            if (interaction.talk && !backstory)
            {

                if (Input.GetKeyDown(KeyCode.Alpha1) && A.activeSelf)
                {
                    ChoseOptionA();
                    Debug.Log("A has been pressed");
                }

                if (Input.GetKeyDown(KeyCode.Alpha2) && B.activeSelf)
                {
                    ChoseOptionB();
                    Debug.Log("B has been pressed");
                }

                DialogProgression();

            }
            else if (!interaction.talk && !interaction.convinced && other.gameObject.tag != "Door")
            {
                ObjectProgression();
            }
            else if (!interaction.talk && !interaction.convinced && other.gameObject.tag == "Door")
            {
                DoorInteraction();
            }
        }
        else
        {
            if(other.gameObject.tag == "Door")
            {
                spaceButtonText.text = "     Leave";
                spaceButton.gameObject.SetActive(true);
                A.SetActive(false);
                B.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Space) && !currentlyTalking)
                {
                    SceneManager.LoadScene("supercity");
                }
            }
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        CloseDialog();
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
            if (optionA1 && !interaction.convinced)
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

            if (optionB1 && !interaction.convinced)
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
            if (optionAA && !interaction.convinced)
            {
                Debug.Log("optionAA is: " + optionAA);
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

            if (optionAB && !interaction.convinced)
            {
                Debug.Log("optionAB is: " + optionAB);
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

            if (optionBA && !interaction.convinced)
            {
                Debug.Log("optionBA is: " + optionBA);
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

            if (optionBB && !interaction.convinced)
            {
                Debug.Log("optionBB is: " + optionBB);
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

    void DoorInteraction()
    {
        text.text = interaction.textObject;
        panel.SetActive(true);
        door = true;
        Debug.Log("Door is: " + door);
    }

    void FinishDialog()
    {
        A.SetActive(false);
        B.SetActive(false);
        spaceButton.gameObject.SetActive(false);
    }

    #region ButtonBehaviour
    public void CloseDialog()
    {
        panel.SetActive(false);
        canvas.gameObject.SetActive(false);
        escButton.gameObject.SetActive(false);
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
        door = false;
    }

    public void StartTalking()
    {
        if (interaction.interactiveObject)
        {
            canvas.gameObject.SetActive(true);
            panel.SetActive(true);
            A.SetActive(false);
            B.SetActive(false);
        }
        else
        {
            if (!interaction.convinced)
            {
                backstory = false;
                anim.SetBool("talking", true);
                panel.SetActive(true);
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
        }
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
    }

    public void Backstory()
    {
        text.text = interaction.textBackstory;
        panel.SetActive(true);
        backstory = true;
    }

}
