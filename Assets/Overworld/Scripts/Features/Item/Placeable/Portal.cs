using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PortalGate : MonoBehaviour
{
    [SerializeField]
    private GameObject targetPortal = default!;

    private Portal portalParent = default!;

    private void Start()
    {
        portalParent = GetComponentInParent<Portal>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!portalParent.CanWarp())
            return;

        if (other.CompareTag("Player") || other.CompareTag("Item"))
        {
            WarpPlayer(other.transform, targetPortal.transform);
        }
    }

    private void WarpPlayer(Transform player, Transform targetWarp)
    {
        portalParent.StartTimer();
        player.position = targetWarp.position;
    }
}
