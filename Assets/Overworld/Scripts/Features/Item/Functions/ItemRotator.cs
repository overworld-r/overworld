using Overworld.Types;
using UnityEngine;

namespace Overworld.Item.Functions
{
    public class ItemRotator : MonoBehaviour
    {
        private IOption<GameObject> heldItem = new None<GameObject>();

        [SerializeField]
        private float rotationSpeed = 90f;

        void Update()
        {
            heldItem.Match(
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

        public void SetHeldItem(IOption<GameObject> item)
        {
            heldItem = item;
        }
    }
}