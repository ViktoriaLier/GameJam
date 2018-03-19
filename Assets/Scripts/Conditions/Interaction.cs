using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

    public bool talk;
    public bool convinced = false;
    public bool interactiveObject;
    public bool correctOrder;

    //TextBox Texts
    public string textInitiate;
    public string textObject;
    public string textChoseA;
    public string textChoseB;
    public string textConvinced;
    public string textChoseA2;
    public string textChoseB2;

    // A/B Answers
    public string textA;
    public string textB;
    //Route A
    public string textAA;
    public string textAB;
    //Route B
    public string textBA;
    public string textBB;

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
            interactiveObject = true;
        }
    }


}
