using Overworld.Features.CustomCollision;
using Overworld.Types;
using UnityEngine;
using Overworld.Features.CustomCollision;
using Overworld.Types;

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
        Debug.Log("OnTriggerEnter2D called with " + other.name);


        other.gameObject.OptGetComponent<CustomCollision>(none: () =>
        {
            if (other.CompareTag("Player") || other.CompareTag("Item"))
            {
                if (!portalParent.CanWarp())
                {
                    Debug.Log("cooltime" + other.name);
                    return;
                }
                WarpPlayer(other.transform, targetPortal.transform);
            }
        });
    }

    private void WarpPlayer(Transform player, Transform targetWarp)
    {
        Debug.Log("俺とんだよ" + player.gameObject.name);
        portalParent.StartTimer();
        player.position = targetWarp.position;
    }
}
