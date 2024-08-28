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
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        itemPointer.locationStatus.Match(bag: () =>
                        {
                            ray = overworldModel.UICamera.ScreenPointToRay(Input.mousePosition);
                        });

                        RaycastHit2D[] hitSprites = Physics2D.RaycastAll(ray.origin, ray.direction);

                        foreach (var sprite in hitSprites)
                        {
                            if (sprite != false && sprite.transform.gameObject.CompareTag("Item"))
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
