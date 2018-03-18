using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

    public bool talk;
    public bool convinced = false;

    private void OnTriggerStay(Collider other)
    {
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


}
