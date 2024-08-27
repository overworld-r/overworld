using Overworld.Core;
using Overworld.Models;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(PlayerPointer))]
    public class PointerMover : MonoBehaviour
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        private PlayerPointer playerPointer = default!;

        void Start()
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

                    playerPointer.locationStatus.Match(
                        world: () =>
                        {
                            item.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
                        },
                        bag: () =>
                        {
                            item.transform.position = overworldModel.UICamera.ScreenToWorldPoint(
                                mousePosition
                            );
                        }
                    );
                }
            );
        }
    }
}
