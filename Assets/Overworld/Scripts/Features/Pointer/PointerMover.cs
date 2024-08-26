using Overworld.Core;
using Overworld.Models;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(PlayerPointer))]
    public class PointerMover : MonoBehaviour
    {
        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [SerializeField]
        private PlayerPointer playerPointer = default!;

        void Reset()
        {
            playerPointer = GetComponent<PlayerPointer>();
        }

        void Update()
        {
            playerPointer.holdingItem.Match(
                none: () =>
                {
                    return;
                },
                some: item =>
                {
                    Vector3 mousePosition = Input.mousePosition;
                    mousePosition.z = 10f;

                    item.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

                    if (playerPointer?.locationStatus == OverworldModel.LocationStatus.World)
                    {
                        item.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
                    }
                    else
                    {
                        if (overworldModel.UICamera != null)
                            item.transform.position = overworldModel.UICamera.ScreenToWorldPoint(
                                mousePosition
                            );
                    }
                }
            );
        }
    }
}
