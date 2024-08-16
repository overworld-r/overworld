using Overworld.Core;
using Overworld.Model;
using UnityEngine;

namespace Overworld.Item
{
    public class ItemPointer : MonoBehaviour
    {
        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();
        GameObject grippingItem;
        ItemBase grippingItemComponent;

        private const float ScreenToWorldPointZ = 10.0f;
        private const float RotationAngle = 90f;
        ItemBase.LocationStatus cursorLocationStatus = ItemBase.LocationStatus.World;
        GameObject Backpack;
        Backpack.Backpack BackpackComponent;

        float LocationLine = Screen.height - Screen.height / 2;

        void Start()
        {
            Backpack = overworldModel.Backpack;
            BackpackComponent = Backpack.GetComponent<Backpack.Backpack>();
        }

        void Update()
        {
            HandleMouseClick();
            HandleCursorLocationStatus();

            if (!grippingItem || !grippingItemComponent || !grippingItemComponent.isHolding)
            {
                return;
            }

            ControlGrippingItemPosition();
            ControlGrippingItemRotation();
            HandleGrippingItemLocationStatus();
        }

        void HandleMouseClick()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            Ray ray =
                cursorLocationStatus == ItemBase.LocationStatus.World
                    ? Camera.main.ScreenPointToRay(Input.mousePosition)
                    : overworldModel.UICamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hitSprite = Physics2D.Raycast(ray.origin, ray.direction);
            if (!hitSprite)
            {
                return;
            }

            GameObject clickedItem = hitSprite.transform.gameObject;
            if (!clickedItem.CompareTag("Item"))
            {
                return;
            }

            if (clickedItem.TryGetComponent<ItemBase>(out var clickedItemComponent))
            {
                var itemPrefab = overworldModel.ItemPrefabs.Find(prefab =>
                    prefab.name == clickedItem.name
                );
                grippingItem = clickedItemComponent.OnClick(itemPrefab);
                if (grippingItem.TryGetComponent<ItemBase>(out var itemComponent))
                {
                    grippingItemComponent = itemComponent;
                }
            }
        }

        void ControlGrippingItemPosition()
        {
            if (grippingItemComponent.locationStatus == ItemBase.LocationStatus.Bag)
            {
                grippingItem.transform.position = overworldModel.UICamera.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenToWorldPointZ)
                );
            }
            else
            {
                grippingItem.transform.position = Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenToWorldPointZ)
                );
            }
        }

        void ControlGrippingItemRotation()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                grippingItem.transform.Rotate(0, 0, RotationAngle);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                grippingItem.transform.Rotate(0, 0, -RotationAngle);
            }
        }

        void HandleCursorLocationStatus()
        {
            if (BackpackComponent.open == false)
            {
                cursorLocationStatus = ItemBase.LocationStatus.World;
                return;
            }

            if (cursorLocationStatus == ItemBase.LocationStatus.Bag)
            {
                if (Input.mousePosition.y < LocationLine)
                    return;

                cursorLocationStatus = ItemBase.LocationStatus.World;
            }
            else
            {
                if (BackpackComponent.open == false)
                    return;

                if (Input.mousePosition.y >= LocationLine)
                    return;

                cursorLocationStatus = ItemBase.LocationStatus.Bag;
            }
        }

        void HandleGrippingItemLocationStatus()
        {
            if (BackpackComponent.open == false)
            {
                return;
            }

            if (grippingItemComponent.locationStatus == ItemBase.LocationStatus.Bag)
            {
                if (Input.mousePosition.y < LocationLine)
                    return;

                grippingItemComponent.ChangeLocationStatus(
                    grippingItem,
                    this.transform,
                    ItemBase.LocationStatus.World
                );
            }
            else
            {
                if (Input.mousePosition.y >= LocationLine)
                    return;

                var canvas = Backpack.transform.Find("Canvas");
                grippingItemComponent.ChangeLocationStatus(
                    grippingItem,
                    canvas.transform,
                    ItemBase.LocationStatus.Bag
                );
            }
        }
    }
}