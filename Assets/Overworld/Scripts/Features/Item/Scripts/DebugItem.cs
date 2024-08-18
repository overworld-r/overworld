using UnityEngine;

namespace Overworld.Item
{
    public class DebugItem : MonoBehaviour, IItemMetadata
    {
        public bool CanBuild { get; private set; } = true;

        string IItemMetadata.itemName { get; set; } = "DebugItem";
        string IItemMetadata.description { get; set; } = "this is a debug item.";
        int IItemMetadata.price { get; set; } = 100;

        protected void Update() { }

        protected void FixedUpdate() { }
    }
}
