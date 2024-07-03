using System.Collections.Generic;
using Overworld.Model;
using UnityEngine;

namespace Overworld.Item
{
    public class ItemPointer : MonoBehaviour
    {
        public List<GameObject> ItemPrefabs = new List<GameObject>();
        public GameObject grippingItem;

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

            clickedItem.TryGetComponent<IItem>(out var clickedItemComponent);
            grippingItem = clickedItemComponent.OnClick(itemPrefab);
        }

        private void HandleGrippingItem()
        {
            if (!grippingItem)
            {
                return;
            }

            grippingItem.TryGetComponent<IItem>(out var itemComponent);
            if (!itemComponent.isHolding)
            {
                return;
            }

            Vector3 screenPosition = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(screenPosition.x, screenPosition.y, ScreenToWorldPointZ)
            );
            grippingItem.transform.position = new Vector3(worldPos.x, worldPos.y, worldPos.z);

            // Processing of rotation of the item being held
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                grippingItem.transform.Rotate(0, 0, RotationAngle);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                grippingItem.transform.Rotate(0, 0, -RotationAngle);
            }
        }
    }
}
