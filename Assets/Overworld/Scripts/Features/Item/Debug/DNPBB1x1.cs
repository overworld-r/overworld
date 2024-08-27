using UnityEngine;

namespace Overworld.Features.Item
{
    using Models;

    public class DNPBB1x1 : MonoBehaviour, IItemMetadata
    {
        public bool CanBuild { get; private set; } = true;

        ItemMetadata IItemMetadata.metadata { get; set; } = new ItemMetadata("DNPBB1x1", "", 100f);
    }
}