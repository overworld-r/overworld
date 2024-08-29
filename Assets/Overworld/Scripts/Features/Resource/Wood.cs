using UnityEngine;

namespace Overworld.Features.Resource
{
    using Models;

    public class Wood : MonoBehaviour, IResourceMetadata
    {
        public ResourceType type { get; set; } = ResourceType.Wood;
    }
}