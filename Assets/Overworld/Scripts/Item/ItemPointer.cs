using System.Collections.Generic;
using UnityEngine;

namespace Overworld.Item
{
    public class ItemPointer : MonoBehaviour
    {
        public List<GameObject> ItemPrefabs = new List<GameObject>();
        public GameObject backpack;
        public GameObject grippingItem;
        public ItemBase grippingItemComponent;
        public Camera UICamera;

        private const float ScreenToWorldPointZ = 10.0f;
        private const float RotationAngle = 90f;
        ItemBase.LocationStatus locationStatus = ItemBase.LocationStatus.World;

        public void Update()
        {
            HandleMouseClick();
            HandleGrippingItem();
        }

        private void HandleMouseClick()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            Ray ray =
                locationStatus == ItemBase.LocationStatus.World
                    ? Camera.main.ScreenPointToRay(Input.mousePosition)
                    : UICamera.ScreenPointToRay(Input.mousePosition);

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
                var itemPrefab = ItemPrefabs.Find(prefab => prefab.name == clickedItem.name);
                grippingItem = clickedItemComponent.OnClick(itemPrefab);
                if (grippingItem.TryGetComponent<ItemBase>(out var itemComponent))
                {
                    grippingItemComponent = itemComponent;
                }
            }
        }

        private void HandleGrippingItem()
        {
            if (!grippingItem || !grippingItemComponent)
            {
                return;
            }

            if (!grippingItemComponent.isHolding)
            {
                return;
            }

            if (grippingItemComponent.locationStatus == ItemBase.LocationStatus.Bag)
            {
                grippingItem.transform.position = UICamera.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenToWorldPointZ)
                );
            }
            else
            {
                grippingItem.transform.position = Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenToWorldPointZ)
                );
            }

            HandleBackpack();

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                grippingItem.transform.Rotate(0, 0, RotationAngle);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                grippingItem.transform.Rotate(0, 0, -RotationAngle);
            }
        }

        void HandleBackpack()
        {
            if (!backpack.TryGetComponent<Backpack.Backpack>(out var backpackComponent))
                return;

            if (backpackComponent.open == false)
                return;

            if (grippingItemComponent.locationStatus == ItemBase.LocationStatus.Bag)
            {
                if (Input.mousePosition.y < Screen.height - Screen.height / 2)
                    return;
                locationStatus = ItemBase.LocationStatus.World;

                grippingItemComponent.ChangeLocationStatus(
                    grippingItem,
                    this.transform,
                    ItemBase.LocationStatus.World
                );
            }
            else
            {
                if (Input.mousePosition.y >= Screen.height - Screen.height / 2)
                    return;

                locationStatus = ItemBase.LocationStatus.Bag;

                var canvas = backpack.transform.Find("Canvas");
                grippingItemComponent.ChangeLocationStatus(
                    grippingItem,
                    canvas.transform,
                    ItemBase.LocationStatus.Bag
                );
            }
        }
    }
}
