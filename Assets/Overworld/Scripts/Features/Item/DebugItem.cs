using UnityEngine;

namespace Overworld.Features.Item
{
    public class DebugItem : MonoBehaviour, Models.IItemMetadata
    {
        public bool CanBuild { get; private set; } = true;

        string Models.IItemMetadata.itemName { get; set; } = "DebugItem";
        string Models.IItemMetadata.description { get; set; } = "this is a debug item.";
        int Models.IItemMetadata.price { get; set; } = 100;

        protected void Update() { }

        protected void FixedUpdate() { }
    }
}