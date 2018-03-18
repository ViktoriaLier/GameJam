using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

    public bool talk;
    public bool convinced = false;
    public bool interactiveObject;

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
