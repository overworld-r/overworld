using UnityEngine;

namespace Overworld.Features.Resource
{
    using Models;

    public class Stone : MonoBehaviour, IResourceMetadata
    {
        public ResourceType type { get; set; } = ResourceType.Stone;
    }
}
