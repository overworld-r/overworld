using Overworld.Core;
using Overworld.Features.Item.Models;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    class Breakable : MonoBehaviour, Models.IClickable
    {
        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();
        ItemBase? itemBase;
        PlayerPointer playerPointer = default!;

        void Awake()
        {
            itemBase = GetComponent<ItemBase>();
            playerPointer = overworldModel.Pointer.GetComponent<PlayerPointer>();
        }

        void Models.IClickable.OnClick(GameObject itemPrefab)
        {
            ItemBase _itemBase = itemBase.Unwrap();

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
