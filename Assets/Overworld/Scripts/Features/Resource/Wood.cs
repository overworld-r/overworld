using UnityEngine;

namespace Overworld.Features.Resource
{
    using Models;

    [RequireComponent(typeof(Resource))]
    public class Wood : MonoBehaviour, IResourceMetadata
    {
        public ResourceType type { get; set; } = ResourceType.Wood;
    }
}
