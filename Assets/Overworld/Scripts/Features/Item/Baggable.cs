using Overworld.Core;
using Overworld.Model;
using UnityEngine;

namespace Overworld.Item
{
    public class Baggable : MonoBehaviour
    {
        public bool canPut = true;
        private ItemBase itemBase;

        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        public void Awake()
        {
            itemBase = GetComponent<ItemBase>();
        }

        public void OnClick()
        {
            if (itemBase.locationStatus != ItemBase.LocationStatus.Bag)
            {
                return;
            }

            if (itemBase.isHolding)
            {
                itemBase.isHolding = false;
            }
            else
            {
                itemBase.isHolding = true;
            }

            Destroy(GetComponent<Rigidbody2D>());
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (itemBase.locationStatus != ItemBase.LocationStatus.World)
            {
                return;
            }

            if (!itemBase.isHolding || !canPut)
            {
                return;
            }
            canPut = false;
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (itemBase.locationStatus != ItemBase.LocationStatus.World)
            {
                return;
            }
            OnTriggerEnter2D(other);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (itemBase.locationStatus != ItemBase.LocationStatus.World)
            {
                return;
            }
            if (!itemBase.isHolding || canPut)
            {
                return;
            }
            canPut = true;
        }
    }
}
