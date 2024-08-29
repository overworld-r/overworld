using System.Collections;
using UnityEngine;
using Overworld.Features.CustomCollision;

namespace Overworld.Features.Item
{
    using Models;

    [RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
    public class BreakGround : MonoBehaviour, IItemMetadata, ICustomCollision
    {
        public const float breakDuration = 3f;
        public const float respawnDuration = 3f;
        private float timeElapsed = 0f;
        private bool playOnFloor = false;
        private Collider2D floorCollider = default!;
        private SpriteRenderer spriteRenderer = default!;
        ItemMetadata IItemMetadata.metadata { get; set; } = new ItemMetadata("BreakGround", "", 0.0f);

        void ICustomCollision.OnCustomCollisionStay(string ID, Collider2D collider)
        {
            if (ID == "0")
            {
                Break(collider);
            }
        }

        private void Start()
        {
            floorCollider = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            spriteRenderer.color = Color.red;
        }

        private void Update()
        {
            if (playOnFloor)
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed >= breakDuration)
                {
                    BreakFloor();
                }
            }
        }


        void Break(Collider2D collider)
        {
                playOnFloor = true;
        }
        

        private void BreakFloor()
        {
            floorCollider.isTrigger = true;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

            playOnFloor = false;

            timeElapsed = 0f;

            StartCoroutine(RespawnFloor());
        }

        private IEnumerator RespawnFloor()
        {
            yield return new WaitForSeconds(respawnDuration);

            floorCollider.isTrigger = false;
            spriteRenderer.color = Color.red;
        }
    }
}
