using Overworld.Core;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(PlayerPointer))]
    public class PointerClickHandler : MonoBehaviour
    {
        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();
        public event System.Action<GameObject>? OnItemClicked;

        private PlayerPointer itemPointer = default!;

        void Start()
        {
            itemPointer = GetComponent<PlayerPointer>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                itemPointer.holdingItem.Match(
                    some: item =>
                    {
                        OnItemClicked.Match(f => f.Invoke(item));
                    },
                    none: () =>
                    {
                        Vector3 worldPoint = itemPointer.locationStatus.Match(
                            world: () => Camera.main.ScreenToWorldPoint(Input.mousePosition),
                            bag: () =>
                                overworldModel.UICamera.ScreenToWorldPoint(Input.mousePosition)
                        );

                        Vector2 worldPoint2D = new Vector2(worldPoint.x, worldPoint.y);

                        RaycastHit2D[] hitSprites = Physics2D.RaycastAll(
                            worldPoint2D,
                            Vector2.zero,
                            100f
                        );

                        foreach (var sprite in hitSprites)
                        {
                            if (sprite.transform.gameObject.CompareTag("Item"))
                            {
                                OnItemClicked.Match(v => v.Invoke(sprite.transform.gameObject));
                                break;
                            }
                        }
                    }
                );
            }
        }
    }
}