using Overworld.Features.CustomCollision;
using Overworld.Features.Player;
using UnityEngine;

namespace Overworld.Features.Item
{
    public class Spring : MonoBehaviour, Models.IItemMetadata, ICustomCollision
    {
        string Models.IItemMetadata.itemName { get; set; } = "Spring";

        string Models.IItemMetadata.description { get; set; } =
            "A spring that bounces the player up when they touch on top surface of it.";

        int Models.IItemMetadata.price { get; set; } = 10;

        public float bounceForce = 1f;

        void ICustomCollision.OnCustomCollisionEnter(string ID, Collider2D collider)
        {
            if (ID == "0")
            {
                Bounce(collider);
            }
        }

        void Bounce(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                PlayerController pc = collider.gameObject.GetComponent<PlayerController>();
                pc.Push(bounceForce * transform.up);
            }
            else
            {
                if (collider.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                {
                    rb.AddForce(bounceForce * transform.up, ForceMode2D.Impulse);
                }
            }
        }
    }
}
