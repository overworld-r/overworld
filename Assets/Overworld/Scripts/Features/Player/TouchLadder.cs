using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    public float climbSpeed = 5f;  // はしごを上る速度
    private bool isClimbing = false;  // プレイヤーがはしごにいるかどうか
    private Rigidbody2D rb;  // プレイヤーのRigidbody2Dコンポーネント

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // はしごにいる場合、スペースキーを押すと上昇する
        if (isClimbing && Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // はしごに触れた場合、isClimbingをtrueにする
        if (collision.GetComponent<Ladder>() != null)
        {
            isClimbing = true;
            rb.gravityScale = 0;  // 重力を無効にする
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // はしごから離れた場合、isClimbingをfalseにする
        if (collision.GetComponent<Ladder>() != null)
        {
            isClimbing = false;
            rb.gravityScale = 1;  // 重力を元に戻す
        }
    }
}
