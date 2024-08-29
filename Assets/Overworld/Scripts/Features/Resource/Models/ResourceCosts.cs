using UnityEngine;

namespace Overworld.Features.Resource.Models
{
    [System.Serializable]
    public class ResourceCost
    {
        [SerializeField]
        public ResourceType type = ResourceType.Stone;

        [SerializeField]
        public int amount = 0;
    }
}
