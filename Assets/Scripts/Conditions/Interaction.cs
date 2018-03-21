using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

    public bool talk;
    public bool convinced = false;
    public bool interactiveObject;
    public bool correctOrder;

    //Name of the NPC
    public string npcName;

    //TextBox Texts
    public string textBackstory;
    public string textInitiate;
    public string textObject;
    public string textChoseA; 
    public string textChoseB;
    public string textConvinced;
    public string textChoseAA;
    public string textChoseBB;
    public string textChoseBA;
    public string textChoseAB;

    // A/B Answers
    public string textA;
    public string textB;
    //Route A
    public string textAA;
    public string textAB;
    //Route B
    public string textBA;
    public string textBB;

    //Button Texts
    public string textButtonA;
    public string textButtonB;
    public string textButtonAA;
    public string textButtonBB;
    public string textButtonAB;
    public string textButtonBA;

    //Correct Orders
    public bool correctAA;
    public bool correctAB;
    public bool correctBA;
    public bool correctBB;

    private void OnTriggerStay(Collider other)
    {
        if(gameObject.tag == "NPC")
        {
            interactiveObject = false;
            if (other.gameObject.tag == "Player")
            {
                if (convinced == false)
                {
                    talk = true;
                }
                else
                {
                    talk = false;
                }
            }
        }
       
        if(gameObject.tag == "Object")
        {
            npcName = "You: ";
            interactiveObject = true;
        }
    }


}
