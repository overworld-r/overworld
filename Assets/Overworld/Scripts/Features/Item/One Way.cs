using UnityEngine;

public class DisableColliderOnButtonPress : MonoBehaviour
{
    public BoxCollider2D objectCollider; // 対象オブジェクトのCollider

    void Start()
    {
        if (objectCollider == null)
        {
            objectCollider = GetComponent<BoxCollider2D>();
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            objectCollider.enabled = false; // ボタンが押されている間、Colliderを無効にする
        }
        else
        {
            objectCollider.enabled = true; // ボタンが押されていない間、Colliderを有効にする
        }
    }
}
