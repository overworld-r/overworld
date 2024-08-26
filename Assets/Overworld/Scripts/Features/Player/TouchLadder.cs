using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using Overworld.Types;

public class PlayerController : MonoBehaviour
{
    public float climbSpeed = 5f;
    private bool isClimbing = false;
    private Rigidbody2D? rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (isClimbing && Input.GetKey(KeyCode.Space))
        {
            rb.Unwrap().velocity = new Vector2(rb.velocity.x, climbSpeed);
        }
        else
        {
            rb.Unwrap().velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Ladder>() != null)
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.GetComponent<Ladder>() != null)
        {
            isClimbing = false;
        }
    }
}
