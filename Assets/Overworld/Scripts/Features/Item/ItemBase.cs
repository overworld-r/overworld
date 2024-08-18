using Overworld.Model;
using UnityEngine;

namespace Overworld.Item
{
    public class ItemBase : MonoBehaviour
    {
        public bool isHolding = false;

        public OverworldModel.LocationStatus locationStatus = OverworldModel.LocationStatus.World;

        public void ChangeLocationStatus(
            GameObject item,
            Transform parent,
            OverworldModel.LocationStatus status
        )
        {
            item.transform.SetParent(parent, false);

            if (item.TryGetComponent<Collider2D>(out var itemCollider))
            {
                itemCollider.isTrigger = true;
            }

            if (item.TryGetComponent<Rigidbody2D>(out var itemRigidbody))
            {
                Destroy(itemRigidbody);
            }

            if (item.TryGetComponent<SpriteRenderer>(out var itemRenderer))
            {
                itemRenderer.sortingOrder = 100;
            }

            if (item.TryGetComponent<ItemBase>(out var itemComponent))
            {
                itemComponent.locationStatus = status;
            }

            if (
                item.TryGetComponent<Placeable>(out var itemPlaceable)
                && status == OverworldModel.LocationStatus.World
            )
            {
                itemPlaceable.canBuild = true;
            }
        }
    }
}
