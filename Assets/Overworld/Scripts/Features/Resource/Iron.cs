using UnityEngine;

namespace Overworld.Features.Resource
{
    using Models;

    public class Iron : MonoBehaviour, IResourceMetadata
    {
        public ResourceType type { get; set; } = ResourceType.Iron;
    }
}