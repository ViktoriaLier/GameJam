using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public Animator anim;
    public Rigidbody rbody;

    private float inputH;
    private float inputV;
    

    void Star()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
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

        float moveX = inputH * 20f * Time.deltaTime;
        float moveZ = inputV * 50f * Time.deltaTime;

        rbody.velocity = new Vector3(moveX, 0f, moveZ);
    }
}
