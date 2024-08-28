using UnityEngine;

namespace Overworld.Features.Item
{
    using Models;

    public class Ball : MonoBehaviour, IItemMetadata
    {
        ItemMetadata IItemMetadata.metadata { get; set; } =
            new ItemMetadata("Ball", "Throwable ball", 100f);

        void Start() { }

        void Update() { }
    }
}
