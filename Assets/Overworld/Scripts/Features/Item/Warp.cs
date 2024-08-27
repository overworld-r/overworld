using UnityEngine;

public class WarpCooldownManager : MonoBehaviour
{
    private float lastWarpTime = -9999f; // 最後にワープした時間
    public float cooldownTime = 0.5f; // クールダウン時間

    // クールダウンが完了しているかどうかを確認する
    public bool CanWarp()
    {
        return Time.time - lastWarpTime >= cooldownTime;
    }

    // ワープした時刻を記録する
    public void SetWarpCooldown()
    {
        lastWarpTime = Time.time;
    }
}
