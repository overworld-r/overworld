using UnityEngine;

namespace Overworld.Features.Item
{
    public class DebugItem : MonoBehaviour, Model.IItemMetadata
    {
        public bool CanBuild { get; private set; } = true;

        string Model.IItemMetadata.itemName { get; set; } = "DebugItem";
        string Model.IItemMetadata.description { get; set; } = "this is a debug item.";
        int Model.IItemMetadata.price { get; set; } = 100;

        protected void Update() { }

        protected void FixedUpdate() { }
    }
}