using Overworld.Types;
using System.Collections;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public string targetPortalName = "Gate"; // ワープ先のポータル名
    private WarpCooldownManager? cooldownManager;

    private void Start()
    {
        // 親オブジェクトにあるWarpCooldownManagerを取得
        cooldownManager = GetComponentInParent<WarpCooldownManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cooldownManager != null && cooldownManager.Unwrap().CanWarp() && (other.CompareTag("Player")|| other.CompareTag("item")))
        {
            // 親オブジェクトからワープ先のポータルを探す
            Transform targetWarp = this.gameObject.transform.parent.Find(targetPortalName);
            
            if (targetWarp != null)
            {
                StartCoroutine(WarpPlayer(other.transform, targetWarp));
            }
            else
            {
                Debug.LogError("ワープ先のポータルが見つかりません: " + targetPortalName);
            }
        }
    }

    private IEnumerator WarpPlayer(Transform player, Transform targetWarp)
    {
        // クールダウンを開始
        cooldownManager.Unwrap().SetWarpCooldown();

        // プレイヤーの位置をワープ先に変更
        player.position = targetWarp.position;

        // 一定時間待つ（ワープ中の二重衝突を防ぐため）
        yield return new WaitForSeconds(cooldownManager.Unwrap().cooldownTime);
    }
}
