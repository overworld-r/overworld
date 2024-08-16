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