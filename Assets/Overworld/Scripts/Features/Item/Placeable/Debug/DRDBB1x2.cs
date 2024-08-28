using UnityEngine;

namespace Overworld.Features.Item
{
    using Models;

    public class DRDBB1x2 : MonoBehaviour, IItemMetadata, IResourceMetadata
    {
        public bool CanBuild { get; private set; } = true;

        ItemMetadata IItemMetadata.metadata { get; set; } = new ItemMetadata("DRDBB1x2", "", 100f);

        ResourceMetadata IResourceMetadata.metadata { get; set; } = new ResourceMetadata(3, 1, 1);
    }
}
