using Overworld.Features.CustomCollision;
using Overworld.Types;
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
        private Rigidbody2D rb;

        void Start()
        {
            // Rigidbodyコンポーネントを取得
            rb = GetComponent<Rigidbody2D>();

            // 最初はisKinematicをtrueにして物理挙動を無効化
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
        }

        void Down(Collider2D collider)
        {
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}

