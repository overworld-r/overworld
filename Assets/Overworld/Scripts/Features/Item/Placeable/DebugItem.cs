using UnityEngine;

namespace Overworld.Features.Item
{
    using Models;

    public class DebugItem : MonoBehaviour, IItemMetadata
    {
        public bool CanBuild { get; private set; } = true;

        ItemMetadata IItemMetadata.metadata { get; set; } =
            new ItemMetadata("DebugItem", "This is a debug item.", 100f);

        protected void Update() { }

        protected void FixedUpdate() { }
    }
}
