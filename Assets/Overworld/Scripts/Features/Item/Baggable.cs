using UnityEngine;

namespace Overworld.Item
{
    public class Baggable : MonoBehaviour
    {
        public void OnClick()
        {
            if (
                !TryGetComponent<ItemBase>(out var itemBase)
                || itemBase.locationStatus != ItemBase.LocationStatus.Bag
            )
            {
                return;
            }

            itemBase.isHolding = !itemBase.isHolding;
            Destroy(GetComponent<Rigidbody2D>());
        }
    }
}
