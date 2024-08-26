using Overworld.Core;
using Overworld.Features.Item.Models;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(ItemBase))]
    public class Baggable : MonoBehaviour, Models.IClickable
    {
        public bool canPut { get; private set; } = true;

        [SerializeField]
        private ItemBase? itemBase;

        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        void Reset()
        {
            itemBase = GetComponent<ItemBase>();
        }

        void IClickable.OnClick(GameObject prefab)
        {
            ItemBase _itemBase = itemBase.Unwrap();

            if (_itemBase.locationStatus != OverworldModel.LocationStatus.Bag)
            {
                return;
            }

            if (_itemBase.isHolding)
            {
                var offset = new Vector3(-26.35f, -190.25f, 10.05f);

                var cursorPosition = overworldModel
                    .UICamera.Unwrap()
                    .ScreenToWorldPoint(
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

                _itemBase.isHolding = false;

                return;
            }
            else
            {
                _itemBase.isHolding = true;
            }

            Destroy(GetComponent<Rigidbody2D>());
            return;
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
