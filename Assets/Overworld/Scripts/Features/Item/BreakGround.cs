using UnityEngine;
using System.Collections;
using Overworld.Types;

public class BreakabelFloor : MonoBehaviour
{
    public float timeToBreak = 3f;
    public float respawnTime = 3f;
    private float timeElapsed = 0f;
    private bool playOnFloor = false;
    private Collider2D? floorCollider;
    private SpriteRenderer? spriteRenderer;

    private void Start()
    {
        // 初期化時に床のコライダーを取得
        floorCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // 初期状態を赤色に設定
        spriteRenderer.color = Color.red;
    }
    private void Update()
    {
        if(playOnFloor)
        {
            timeElapsed += Time.deltaTime;

            if(timeElapsed>= timeToBreak)
            {
                BreakFloor();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playOnFloor = true;
        }
    }
    private void BreakFloor()
    {
        Debug.Log("壊れた！");
        floorCollider.Unwrap().isTrigger = true;
        spriteRenderer.Unwrap().color = new Color(1f, 1f, 1f, 0f);

        playOnFloor=false;

        timeElapsed = 0f;
        
        StartCoroutine(RespawnFloor());
    }

    private IEnumerator RespawnFloor()
    {

        yield return new WaitForSeconds(respawnTime);

        floorCollider.Unwrap().isTrigger = false;
        spriteRenderer.Unwrap().color = Color.red;
        
        Debug.Log("直った！");

    }   

}