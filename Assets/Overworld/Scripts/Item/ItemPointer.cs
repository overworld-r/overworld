using System.Collections.Generic;
using Backpack;
using Overworld.Model;
using UnityEngine;

namespace Overworld.Item
{
    public class ItemPointer : MonoBehaviour
    {
        public List<GameObject> ItemPrefabs = new List<GameObject>();
        public GameObject backpack;
        public GameObject grippingItem;
        public IItem grippingItemComponent;

        private const float ScreenToWorldPointZ = 10.0f;
        private const float RotationAngle = 90f;

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

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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

            var itemPrefab = ItemPrefabs.Find(prefab => prefab.name == clickedItem.name);

            if (clickedItem.TryGetComponent<IItem>(out var clickedItemComponent))
            {
                grippingItem = clickedItemComponent.OnClick(itemPrefab, this.transform);

                if (grippingItem.TryGetComponent<IItem>(out var itemComponent))
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

            Vector3 screenPos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(screenPos.x, screenPos.y, ScreenToWorldPointZ)
            );

            if (grippingItemComponent.itemLocationStatus == IItem.ItemLocationStatus.Bag)
            {
                grippingItem.transform.localPosition = new Vector3(
                    screenPos.x,
                    screenPos.y,
                    screenPos.z
                );
            }
            else
            {
                grippingItem.transform.position = new Vector3(worldPos.x, worldPos.y, worldPos.z);
            }

            BackpackProcess(screenPos);

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                grippingItem.transform.Rotate(0, 0, RotationAngle);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                grippingItem.transform.Rotate(0, 0, -RotationAngle);
            }
        }

        void BackpackProcess(Vector3 mousePosition)
        {
            if (!backpack.TryGetComponent<Backpack.Backpack>(out var backpackComponent))
                return;

            if (backpackComponent.open == false)
                return;

            if (grippingItemComponent.itemLocationStatus == IItem.ItemLocationStatus.Bag)
            {
                if (mousePosition.y < 390)
                    return;

                var itemPrefab = ItemPrefabs.Find(prefab => prefab.name == grippingItem.name);
                grippingItem = grippingItemComponent.ChangeLocationStatus(
                    itemPrefab,
                    this.transform,
                    IItem.ItemLocationStatus.World
                );
            }
            else
            {
                if (mousePosition.y >= 390)
                    return;

                var itemPrefab = ItemPrefabs.Find(prefab => prefab.name == grippingItem.name);
                var canvas = backpack.transform.Find("Canvas");
                grippingItem = grippingItemComponent.ChangeLocationStatus(
                    itemPrefab,
                    canvas.transform,
                    IItem.ItemLocationStatus.Bag
                );
            }
        }
    }
}
