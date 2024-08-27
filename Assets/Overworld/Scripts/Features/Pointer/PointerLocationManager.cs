using Overworld.Core;
using Overworld.Models;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(PlayerPointer))]
    public class PointerLocationManager : MonoBehaviour
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [SerializeField]
        private Backpack.Backpack backpackComponent = default!;

        [SerializeField]
        private PlayerPointer playerPointer = default!;

        [SerializeField]
        private Transform bagParent = default!;

        private float locationLine = Screen.height - Screen.height / 2;

        void Reset()
        {
            backpackComponent = overworldModel.Backpack.GetComponent<Backpack.Backpack>();
            playerPointer = GetComponent<PlayerPointer>();

            var canvasName = overworldModel.CanvasObjectName;
            bagParent = overworldModel.Backpack.transform.Find(canvasName).transform;
        }

        public void UpdatePointerLocation()
        {
            if (backpackComponent.open == false)
            {
                playerPointer.locationStatus.value = LocationStatus.Location.World;
                return;
            }

            playerPointer.locationStatus.Match(
                bag: () =>
                {
                    if (Input.mousePosition.y >= locationLine)
                    {
                        playerPointer.locationStatus.value = LocationStatus.Location.World;
                        playerPointer.holdingItem.Match(
                            some: (item) =>
                            {
                                item.transform.SetParent(this.transform, false);
                                if (item.TryGetComponent<Collider2D>(out var itemCollider))
                                {
                                    itemCollider.isTrigger = true;
                                }

                                item.AddComponent<Rigidbody2D>();

                                if (item.TryGetComponent<SpriteRenderer>(out var itemRenderer))
                                {
                                    itemRenderer.sortingOrder = 100;
                                }
                            }
                        );
                    }
                },
                world: () =>
                {
                    if (Input.mousePosition.y < locationLine)
                    {
                        playerPointer.locationStatus.value = LocationStatus.Location.Bag;
                        playerPointer.holdingItem.Match(
                            some: (item) =>
                            {
                                item.transform.SetParent(bagParent, false);
                                if (item.TryGetComponent<Collider2D>(out var itemCollider))
                                {
                                    itemCollider.isTrigger = true;
                                }

                                if (item.TryGetComponent<Rigidbody2D>(out var itemRigidbody))
                                {
                                    Destroy(itemRigidbody);
                                }

                                if (item.TryGetComponent<SpriteRenderer>(out var itemRenderer))
                                {
                                    itemRenderer.sortingOrder = 100;
                                }

                                if (item.TryGetComponent<Placeable>(out var itemPlaceable))
                                {
                                    itemPlaceable.canBuild = true;
                                }
                            }
                        );
                    }
                }
            );
        }
    }
}