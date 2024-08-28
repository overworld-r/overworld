using Overworld.Features.CustomCollision;
using UnityEngine;

namespace Overworld.Features.Item
{
    using Models;
    using Overworld.Mechanics.Types;

    public class DashBoard : MonoBehaviour, IItemMetadata, ICustomCollision
    {
        ItemMetadata IItemMetadata.metadata { get; set; } =
            new ItemMetadata("DashBoard", "A dashboard!", 20f);

        public float dashForce = 10f;

        void ICustomCollision.OnCustomCollisionStay(string ID, Collider2D collider)
        {
            if (ID == "0")
            {
                Dash(collider);
            }
        }

        void Dash(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.Push(dashForce * -transform.right, ForceMode2D.Impulse);
            }
        }
    }
}
