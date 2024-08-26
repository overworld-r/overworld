using Overworld.Features.CustomCollision;
using Overworld.Features.Player;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Item
{
    public class DashBoard : MonoBehaviour, Models.IItemMetadata, ICustomCollision
    {
        string Models.IItemMetadata.itemName { get; set; } = "DashBoard";

        string Models.IItemMetadata.description { get; set; } = "A dashboard!";

        int Models.IItemMetadata.price { get; set; } = 20;

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
            if (collider.gameObject.tag != "Player")
            {
                if (collider.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                {
                    rb.AddForce(dashForce * -transform.right, ForceMode2D.Impulse);
                }
            }
            else
            {
                PlayerController pc = collider.gameObject.GetComponent<PlayerController>();
                pc.Push(dashForce * -transform.right);
            }
        }
    }
}