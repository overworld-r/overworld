using UnityEngine;

namespace Overworld.Features.Item
{
    using Models;

    public class DRDBB1x2 : MonoBehaviour, IItemMetadata
    {
        public bool CanBuild { get; private set; } = true;

        ItemMetadata IItemMetadata.metadata { get; set; } = new ItemMetadata("DRDBB1x2", "", 100f);
    }
}
