using Overworld.Model;
using UnityEngine;

namespace Overworld.Item
{
    public class ItemPointer : MonoBehaviour
    {
        public GameObject grippingItem;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hitSprite = Physics2D.Raycast(ray.origin, ray.direction);
                if (hitSprite)
                {
                    GameObject clickedItem = hitSprite.transform.gameObject;
                    ItemGeneration clickedItemGeneration =
                        clickedItem.GetComponent<ItemGeneration>();

                    if (clickedItem.CompareTag("Item"))
                    {
                        grippingItem = clickedItemGeneration.OnClicked();
                    }
                }
            }

            if (!grippingItem)
            {
                return;
            }

            ItemGeneration grippingItemGeneration = grippingItem.GetComponent<ItemGeneration>();
            if (grippingItemGeneration.existanceStatus == ItemGeneration.ExistanceStatus.Ghost)
            {
                Vector3 screenPosition = Input.mousePosition;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(
                    new Vector3(screenPosition.x, screenPosition.y, 10.0f)
                );
                grippingItem.transform.position = new Vector3(worldPos.x, worldPos.y, worldPos.z);
                // Processing of rotation of the item being held
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    grippingItem.transform.Rotate(0, 0, 90);
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    grippingItem.transform.Rotate(0, 0, -90);
                }
            }
        }
    }
}
