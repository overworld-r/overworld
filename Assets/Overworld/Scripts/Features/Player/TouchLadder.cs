using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Item
{
    public class TouchLadder : MonoBehaviour
    {
        public float climbSpeed = 5f;
        private bool isClimbing = false;

        private Rigidbody2D rb = default!;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (isClimbing && Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.OptGetComponent<Ladder>(_ => isClimbing = true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            collision.gameObject.OptGetComponent<Ladder>(_ => isClimbing = false);
        }
    }
}
