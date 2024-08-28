using Overworld.Core;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    using Models;

    [RequireComponent(typeof(BoxCollider2D))]
    public class Throwable : MonoBehaviour, IClickable
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [SerializeField]
        private bool Duplicatable = default!;

        [SerializeField]
        private float ThrowPower = 2.5f;

        private Material? TrunslucentShader;
        private Material? HighlightRedShader;
        private SpriteRenderer? spriteRenderer;

        private PlayerPointer? playerPointer;

        void Start()
        {
            TrunslucentShader = new Material(Shader.Find("unlit/Translucent"));
            HighlightRedShader = new Material(Shader.Find("Unlit/HighlightRed"));
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerPointer = overworldModel.Pointer.GetComponent<PlayerPointer>();
        }

        void IClickable.OnClick(GameObject itemPrefab)
        {
            if (
                playerPointer?.locationStatus.value != LocationStatus.Location.World
                || playerPointer.holdingItem.IsEmpty
            )
            {
                return;
            }

            var newObject = Instantiate(itemPrefab, this.transform.parent);
            newObject.name = this.gameObject.name;
            newObject.transform.position = overworldModel.Player.transform.position;
            newObject.transform.rotation = this.gameObject.transform.rotation;

            newObject.OptGetComponent<Collider2D>(some: collider =>
            {
                collider.isTrigger = playerPointer.holdingItem.IsEmpty;
            });

            newObject.OptGetComponent<Rigidbody2D>(none: () =>
            {
                if (playerPointer.holdingItem.IsEmpty)
                    newObject.AddComponent<Rigidbody2D>();
            });

            newObject.OptGetComponent<SpriteRenderer>(some: renderer =>
            {
                // renderer.material = itemBase.isHolding
                //     ? TrunslucentShader
                //     : new Material(Shader.Find("Sprites/Default"));
            });

            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var force = mousePosition - newObject.transform.position;
            force.z = 0;
            newObject.transform.position += force.normalized;
            newObject
                .gameObject.GetComponent<Rigidbody2D>()
                .AddForce(force * ThrowPower, ForceMode2D.Impulse);

            if (!Duplicatable)
            {
                Destroy(this.gameObject);

                playerPointer.holdingItem.Match(
                    some: (_) =>
                    {
                        playerPointer.holdingItem = new None<GameObject>();
                    },
                    none: () =>
                    {
                        playerPointer.holdingItem = new Some<GameObject>(newObject);
                    }
                );
            }
        }
    }
}
