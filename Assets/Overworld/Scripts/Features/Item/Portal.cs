using Overworld.Types;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PortalGate : MonoBehaviour
{
    [SerializeField]
    private string targetPortalName = "";
    private Portal? portalParent;

    private void Start()
    {
        portalParent = GetComponentInParent<Portal>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (portalParent.Unwrap().CanWarp())
        {
            if (!other.CompareTag("Player") && !other.CompareTag("Item"))
                return;

            Transform targetWarp = this.gameObject.transform.parent.Find(targetPortalName);

            if (targetWarp == null)
            {
                Debug.LogError("ワープ先のポータルが見つかりません: " + targetPortalName);
            }
            else
            {
                WarpPlayer(other.transform, targetWarp);
            }
        }
    }

    private void WarpPlayer(Transform player, Transform targetWarp)
    {
        portalParent?.StartTimer();
        player.position = targetWarp.position;
    }
}
