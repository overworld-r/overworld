using UnityEngine;

public class Portal : MonoBehaviour
{
    private float lastWarpTime = 0.0f;
    private bool shouldStopTimer = false;

    public const float cooldownTime = 2f;

    void Start()
    {
        lastWarpTime = Time.time - cooldownTime;
    }

    public bool CanWarp()
    {
        if (shouldStopTimer)
        {
            lastWarpTime = Time.time;
            return false;
        }

        if (Time.time - lastWarpTime >= cooldownTime)
        {
            StopTimer();
            return true;
        }

        return false;
    }

    public void StartTimer()
    {
        lastWarpTime = Time.time;
        shouldStopTimer = false;
    }

    public void StopTimer()
    {
        shouldStopTimer = true;
    }
}
