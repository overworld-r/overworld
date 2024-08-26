using Overworld.Core;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(PlayerPointer))]
    public class PointerClickHandler : MonoBehaviour
    {
        public event System.Action<GameObject>? OnItemClicked;
        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [SerializeField]
        public PlayerPointer? itemPointer;

        void Reset()
        {
            itemPointer = GetComponent<PlayerPointer>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                itemPointer
                    .Unwrap()
                    .holdingItem.Match(
                        some: item =>
                        {
                            OnItemClicked?.Invoke(item);
                        },
                        none: () =>
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                            if (
                                itemPointer?.locationStatus == OverworldModel.LocationStatus.Bag
                                && overworldModel.UICamera != null
                            )
                            {
                                ray = overworldModel.UICamera.ScreenPointToRay(Input.mousePosition);
                            }

                            RaycastHit2D hitSprite = Physics2D.Raycast(ray.origin, ray.direction);

                            if (
                                hitSprite != false
                                && hitSprite.transform.gameObject.CompareTag("Item")
                            )
                            {
                                OnItemClicked?.Invoke(hitSprite.transform.gameObject);
                            }
                        }
                    );
            }
        }
    }
}
