using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OneWay : MonoBehaviour
{
    private BoxCollider2D objectCollider = default!;
    private Collider2D playerCollider = default!;

    private bool isPlayerTouching = false;

    private void Start()
    {
        objectCollider = GetComponent<BoxCollider2D>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }

    private void Update()
    {
        // プレイヤーが触れていて、かつキーが押されたときに衝突を無効化する
        if (
            isPlayerTouching && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        )
        {
            Physics2D.IgnoreCollision(objectCollider, playerCollider, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == playerCollider)
        {
            // プレイヤーが触れたとき
            isPlayerTouching = true;
        }
    }

    public void Exit()
    {
        isPlayerTouching = false;
        Physics2D.IgnoreCollision(objectCollider, playerCollider, false);
    }
}
