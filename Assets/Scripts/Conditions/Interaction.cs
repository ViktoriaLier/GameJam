using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

    public bool talk;
    public bool convinced = false;

    private void OnTriggerEnter()
    {
        if ( convinced == false)
        {
            talk = true;

        }

    }


}
