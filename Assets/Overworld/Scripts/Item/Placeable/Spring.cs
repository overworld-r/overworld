using Overworld.Model;
using UnityEngine;

namespace Overworld.Item
{
    public class Spring : ItemPlaceable
    {
        public override string itemName => "Spring";

        public override string description =>
            "A spring that bounces the player up when they touch on top surface of it.";

        public override int price => 10;

        public float bounceForce = 10f;

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRigidbody != null)
                {
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, bounceForce);
                }
            }
        }
    }
}
