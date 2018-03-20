using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public Animator anim;
   

    private float inputH;
    private float inputV;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        
    }


    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            anim.Play("Walk", -1, 1f);
        }

        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
        
    }
}
