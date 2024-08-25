using Overworld.Features.Item.Models;
using Overworld.Models;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    class Breakable : MonoBehaviour, Models.IClickable
    {
        ItemBase? itemBase;

        void Awake()
        {
            itemBase = GetComponent<ItemBase>();
        }

        void Models.IClickable.OnClick(GameObject itemPrefab)
        {
            if (
                itemBase?.locationStatus != OverworldModel.LocationStatus.World
                || itemBase.isHolding
            )
            {
                return;
            }

            Destroy(this.gameObject);
            return;
        }

        void OnBreak() { }
    }
}
