using UnityEngine;

namespace Overworld.Item
{
    public class Spring : MonoBehaviour, IItemMetadata
    {
        string IItemMetadata.itemName { get; set; } = "Spring";
        string IItemMetadata.description { get; set; } =
            "A spring that bounce the player up when the touch on top surface";
        int IItemMetadata.price { get; set; } = 10;

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