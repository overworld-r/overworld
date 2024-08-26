using Overworld.Features.Item.Models;
using Overworld.Models;
using Overworld.Types;
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
            ItemBase _itemBase = itemBase.Unwrap();

            if (
                _itemBase.locationStatus != OverworldModel.LocationStatus.World
                || _itemBase.isHolding
            )
            {
                return;
            }

            Destroy(this.gameObject);
            return;
        }
    }
}
