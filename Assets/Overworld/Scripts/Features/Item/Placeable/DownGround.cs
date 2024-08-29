using Overworld.Features.CustomCollision;
using UnityEngine;

namespace Overworld.Features.Item
{
    using Models;
    using Overworld.Mechanics.Types;

    public class DownGround : MonoBehaviour, IItemMetadata, ICustomCollision
    {
        ItemMetadata IItemMetadata.metadata { get; set; } = new ItemMetadata("DownGround", "", 0.0f);

        void ICustomCollision.OnCustomCollisionStay(string ID, Collider2D collider)
        {
            if (ID == "0")
            {
                Down(collider);
            }
        }
        private Rigidbody2D rb=default!;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
        }

        void Down(Collider2D collider)
        {
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}

