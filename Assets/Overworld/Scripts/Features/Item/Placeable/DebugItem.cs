using UnityEngine;

namespace Overworld.Features.Item
{
    using Models;

    public class DebugItem : MonoBehaviour, IItemMetadata, IResourceMetadata
    {
        public bool CanBuild { get; private set; } = true;

        ItemMetadata IItemMetadata.metadata { get; set; } =
            new ItemMetadata("DebugItem", "This is a debug item.", 100f);

        ResourceMetadata IResourceMetadata.metadata { get; set; } = new ResourceMetadata(3, 1, 1);

        protected void Update() { }

        protected void FixedUpdate() { }
    }
}
