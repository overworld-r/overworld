using Overworld.Features.Item.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(PlayerPointer))]
    public class PointerRotator : MonoBehaviour
    {
        private float rotationSpeed = 90f;

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
                    float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
                    if (scrollWheel != 0)
                    {
                        item.OptGetComponent<ICustomRotate>(
                            some: rotate =>
                            {
                                rotate.OnRotate(scrollWheel);
                            },
                            none: () =>
                            {
                                item.transform.Rotate(0, 0, scrollWheel * rotationSpeed);
                            }
                        );

                        // if (item.TryGetComponent<ICustomRotate>(out var rotate))
                        // {
                        //     rotate.OnRotate(scrollWheel);
                        // }
                        // else
                        // {
                        //     item.transform.Rotate(0, 0, scrollWheel * rotationSpeed);
                        // }
                    }
                }
            );
        }
    }
}
