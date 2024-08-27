using Overworld.Features.CustomCollision;
using UnityEngine;

namespace Overworld.Features.Item
{
    using Models;
    using Overworld.Mechanics.Types;

    public class Spring : MonoBehaviour, IItemMetadata, ICustomCollision
    {
        ItemMetadata IItemMetadata.metadata { get; set; } = new ItemMetadata("Spring", "", 0.0f);

        public float bounceForce = 1f;

        void ICustomCollision.OnCustomCollisionStay(string ID, Collider2D collider)
        {
            if (ID == "0")
            {
                Bounce(collider);
            }
        }

        void Bounce(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.Push(bounceForce * transform.up, ForceMode2D.Impulse);
            }
        }
    }
}
