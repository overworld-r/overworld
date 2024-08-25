using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(PlayerPointer))]
    public class PointerRotator : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed = 90f;

        [SerializeField]
        private PlayerPointer? playerPointer;

        void Reset()
        {
            playerPointer = GetComponent<PlayerPointer>();
        }

        void Update()
        {
            if (playerPointer == null)
            {
                throw new System.Exception("PlayerPointer is null");
            }

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
                        item.transform.Rotate(0, 0, scrollWheel * rotationSpeed);
                    }
                }
            );
        }
    }
}
