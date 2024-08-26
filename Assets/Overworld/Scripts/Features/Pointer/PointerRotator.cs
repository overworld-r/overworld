using Overworld.Features.Item.Models;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(PlayerPointer))]
    public class PointerRotator : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed = 90f;

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
                    float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
                    if (scrollWheel != 0)
                    {
                        if (item.TryGetComponent<ICustomRotate>(out var rotate))
                        {
                            rotate.OnRotate(scrollWheel);
                        }
                        else
                        {
                            item.transform.Rotate(0, 0, scrollWheel * rotationSpeed);
                        }
                    }
                }
            );
        }
    }
}
