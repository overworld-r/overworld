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
