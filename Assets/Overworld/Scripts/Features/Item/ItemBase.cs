using UnityEngine;

namespace Overworld.Item
{
    public abstract class ItemBase : MonoBehaviour
    {
        public virtual string itemName { get; set; }
        public virtual string description { get; set; }
        public virtual int price { get; set; }
        public bool isHolding = false;

        public enum LocationStatus
        {
            Bag,
            World,
        }

        public LocationStatus locationStatus = LocationStatus.World;

        public void ChangeLocationStatus(GameObject item, Transform parent, LocationStatus status)
        {
            item.transform.parent = parent.transform;

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
                && status == LocationStatus.World
            )
            {
                itemPlaceable.canBuild = true;
            }
        }

        public GameObject OnClick(GameObject itemPrefab)
        {
            Debug.Log("a");
            if (TryGetComponent<Baggable>(out var baggable))
            {
                baggable.OnClick();
            }

            if (TryGetComponent<Placeable>(out var placeable))
            {
                return placeable.OnClick(itemPrefab);
            }

            return this.gameObject;
        }
    }
}
