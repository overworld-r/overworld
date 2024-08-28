using UnityEngine;

namespace Overworld.Features.Resource
{
    using Models;

    [RequireComponent(typeof(Resource))]
    public class Iron : MonoBehaviour, IResourceMetadata
    {
        public ResourceType type { get; set; } = ResourceType.Iron;
    }
}
