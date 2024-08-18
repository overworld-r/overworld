using Overworld.Model;
using UnityEngine;

namespace Overworld.Item
{
    [RequireComponent(typeof(ItemBase))]
    class Breakable : MonoBehaviour
    {
        ItemBase? itemBase;

        void Awake()
        {
            itemBase = GetComponent<ItemBase>();
        }

        public void OnClick()
        {
            if (
                itemBase?.locationStatus != OverworldModel.LocationStatus.World
                || itemBase.isHolding
            )
            {
                return;
            }

            Destroy(this.gameObject);
        }

        void OnBreak() { }
    }
}
