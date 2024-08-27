using UnityEngine;

namespace Overworld.Features.Item
{
    using Models;

    public class DNDBB1x2 : MonoBehaviour, IItemMetadata
    {
        public bool CanBuild { get; private set; } = true;

        ItemMetadata IItemMetadata.metadata { get; set; } = new ItemMetadata("DNDBB1x2", "", 100f);
    }
}
