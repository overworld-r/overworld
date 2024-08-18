using Overworld.Core;
using Overworld.Item.Model;
using Overworld.Model;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Item
{
    [RequireComponent(typeof(ItemBase))]
    public class Baggable : MonoBehaviour, IClickable
    {
        public bool canPut { get; private set; } = true;

        [SerializeField]
        private ItemBase? itemBase;

        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        void Reset()
        {
            itemBase = GetComponent<ItemBase>();
        }

        IOption<GameObject> IClickable.OnClick(GameObject prefab)
        {
            if (
                itemBase?.locationStatus != OverworldModel.LocationStatus.Bag
                || overworldModel.UICamera == null
            )
            {
                return new Some<GameObject>(this.gameObject);
            }

            if (itemBase.isHolding)
            {
                var offset = new Vector3(-26.35f, -190.25f, 10.05f);

                var cursorPosition = overworldModel.UICamera.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)
                );

                float gridSize = 0.05f * 19f;

                float offsetX = 0.05f;

                float offsetY = 0.15f;

                Renderer renderer = this.gameObject.GetComponent<Renderer>();

                float itemWidth = renderer.bounds.size.x;

                float itemHeight = renderer.bounds.size.y;

                float snappedX =
                    Mathf.Round((cursorPosition.x - offset.x - itemWidth / 2) / gridSize) * gridSize
                    + offset.x
                    + itemWidth / 2f
                    - offsetX;

                float snappedY =
                    Mathf.Round((cursorPosition.y - offset.y - itemHeight / 2) / gridSize)
                        * gridSize
                    + offset.y
                    + itemHeight / 2f
                    + offsetY;

                this.gameObject.transform.position = new Vector3(
                    snappedX,
                    snappedY,
                    cursorPosition.z
                );

                itemBase.isHolding = false;

                return new None<GameObject>();
            }
            else
            {
                itemBase.isHolding = true;
            }

            Destroy(GetComponent<Rigidbody2D>());
            return new Some<GameObject>(this.gameObject);
        }

        // public void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (itemBase.locationStatus != ItemBase.LocationStatus.World)
        //     {
        //         return;
        //     }
        //
        //     if (!itemBase.isHolding || !canPut)
        //     {
        //         return;
        //     }
        //
        //     canPut = false;
        // }
        //
        // public void OnTriggerStay2D(Collider2D other)
        // {
        //     if (itemBase.locationStatus != ItemBase.LocationStatus.World)
        //     {
        //         return;
        //     }
        //
        //     OnTriggerEnter2D(other);
        // }
        //
        // public void OnTriggerExit2D(Collider2D other)
        // {
        //     if (itemBase.locationStatus != ItemBase.LocationStatus.World)
        //     {
        //         return;
        //     }
        //
        //     if (!itemBase.isHolding || canPut)
        //     {
        //         return;
        //     }
        //
        //     canPut = true;
        // }
    }
}
