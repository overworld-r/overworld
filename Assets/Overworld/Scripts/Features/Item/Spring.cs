using UnityEngine;

namespace Overworld.Features.Item
{
    public class Spring : MonoBehaviour, Models.IItemMetadata
    {
        string Models.IItemMetadata.itemName { get; set; } = "Spring";
        string Models.IItemMetadata.description { get; set; } =
            "A spring that bounce the player up when the touch on top surface";
        int Models.IItemMetadata.price { get; set; } = 10;

        public float bounceForce = 10f;

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                return;
            }

            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, bounceForce);
            }
        }
    }
}
