using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string groundTag = "Ground";
    public bool isGround
    {
        get;
        private set;
    }


    void Start()
    {
        
    }

    void Update()
    {

    }


    void onGroundEnter()
    {
        isGround = true;
    }

    void onGroundEStay()
    {
        isGround = true;
    }


    void onGroundExit()
    {
        isGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == groundTag)
        {
            onGroundEnter();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == groundTag)
        {
            onGroundEStay();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == groundTag)
        {
            onGroundExit();
        }
    }
}