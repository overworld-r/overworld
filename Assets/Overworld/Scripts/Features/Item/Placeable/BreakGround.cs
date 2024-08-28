using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class BreakGround : MonoBehaviour
{
    public const float breakDuration = 3f;
    public const float respawnDuration = 3f;
    private float timeElapsed = 0f;
    private bool playOnFloor = false;
    private Collider2D floorCollider = default!;
    private SpriteRenderer spriteRenderer = default!;

    private void Start()
    {
        floorCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = Color.red;
    }

    private void Update()
    {
        if (playOnFloor)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= breakDuration)
            {
                BreakFloor();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playOnFloor = true;
        }
    }

    private void BreakFloor()
    {
        Debug.Log("壊れた！");
        floorCollider.isTrigger = true;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

        playOnFloor = false;

        timeElapsed = 0f;

        StartCoroutine(RespawnFloor());
    }

    private IEnumerator RespawnFloor()
    {
        yield return new WaitForSeconds(respawnDuration);

        floorCollider.isTrigger = false;
        spriteRenderer.color = Color.red;

        Debug.Log("直った！");
    }
}
