using UnityEngine;

namespace Overworld.Model
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class IItem : MonoBehaviour
    {
        public enum ItemLocationStatus
        {
            Bag,
            World,
        }

        public bool isHolding = false;

        public abstract string itemName { get; }
        public abstract string description { get; }
        public abstract int price { get; }

        public ItemLocationStatus itemLocationStatus;

        public abstract GameObject OnClick(GameObject itemPrefab, Transform parent);
        public abstract GameObject ChangeLocationStatus(
            GameObject itemPrefab,
            Transform parent,
            ItemLocationStatus status
        );
    }
}
