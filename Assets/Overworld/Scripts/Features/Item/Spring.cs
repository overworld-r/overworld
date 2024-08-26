using Overworld.CustomCollision;
using Overworld.Features.Player;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Item
{
    public class Spring : MonoBehaviour, Models.IItemMetadata, ICustomCollision
    {
        string Models.IItemMetadata.itemName { get; set; } = "Spring";

        string Models.IItemMetadata.description { get; set; } =
            "A spring that bounces the player up when they touch on top surface of it.";

        int Models.IItemMetadata.price { get; set; } = 10;

        public bool CanBuild { get; set; } = true;

        public float bounceForce = 1f;

        void ICustomCollision.OnCustomCollision(string ID, Collider2D collider)
        {
            if (ID == "0")
            {
                Bounce(collider);
            }
        }

        void Bounce(Collider2D collider)
        {
            if (collider.gameObject.tag != "Player")
            {
                return;
            }

            PlayerController pc = collider.gameObject.GetComponent<PlayerController>().Unwrap();
            pc.Push(bounceForce * transform.up);
        }
    }
}