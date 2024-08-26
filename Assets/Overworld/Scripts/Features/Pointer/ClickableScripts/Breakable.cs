using Overworld.Core;
using Overworld.Models;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(BoxCollider2D))]
    class Breakable : MonoBehaviour, Models.IClickable
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();
        private PlayerPointer playerPointer = default!;

        void Awake()
        {
            playerPointer = overworldModel.Pointer.GetComponent<PlayerPointer>();
        }

        void Models.IClickable.OnClick(GameObject itemPrefab)
        {
            if (
                playerPointer.locationStatus.value != LocationStatus.Location.World
                || !playerPointer.holdingItem.IsEmpty
            )
            {
                return;
            }

            Destroy(this.gameObject);
            return;
        }
    }
}
