using Overworld.Core;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Placeable : MonoBehaviour, IClickable
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        private Material? TrunslucentShader;
        private Material? HighlightRedShader;
        private SpriteRenderer? spriteRenderer;

        private PlayerPointer playerPointer = default!;

        public bool canBuild = true;

        public void Awake()
        {
            TrunslucentShader = new Material(Shader.Find("unlit/Translucent"));
            HighlightRedShader = new Material(Shader.Find("Unlit/HighlightRed"));
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerPointer = overworldModel.Pointer.GetComponent<PlayerPointer>();
        }

        void IClickable.OnClick(GameObject itemPrefab)
        {
            if (playerPointer.locationStatus.value != LocationStatus.Location.World || !canBuild)
            {
                return;
            }

            var newObject = Instantiate(itemPrefab, this.transform.parent);
            newObject.name = this.gameObject.name;
            newObject.transform.position = this.gameObject.transform.position;
            newObject.transform.rotation = this.gameObject.transform.rotation;

            if (newObject.TryGetComponent<Collider2D>(out var collider))
            {
                collider.isTrigger = playerPointer.holdingItem.IsEmpty;
            }

            if (
                playerPointer.holdingItem.IsEmpty
                && !newObject.TryGetComponent<Rigidbody2D>(out var rigidbody)
            )
            {
                newObject.AddComponent<Rigidbody2D>();
            }

            if (newObject.TryGetComponent<SpriteRenderer>(out var renderer))
            {
                // renderer.material = itemBase.isHolding
                //     ? TrunslucentShader
                //     : new Material(Shader.Find("Sprites/Default"));
            }

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

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (
                playerPointer.locationStatus.value != LocationStatus.Location.World
                || playerPointer.holdingItem != new Some<GameObject>(this.gameObject)
            )
            {
                return;
            }

            if (playerPointer.holdingItem.IsEmpty || !canBuild)
            {
                return;
            }
            // spriteRenderer.material = HighlightRedShader;
            canBuild = false;
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (
                playerPointer.locationStatus.value != LocationStatus.Location.World
                || playerPointer.holdingItem != new Some<GameObject>(this.gameObject)
            )
            {
                return;
            }
            OnTriggerEnter2D(other);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (
                playerPointer.locationStatus.value != LocationStatus.Location.World
                || playerPointer.holdingItem != new Some<GameObject>(this.gameObject)
            )
            {
                return;
            }
            if (playerPointer.holdingItem.IsEmpty || canBuild)
            {
                return;
            }
            // spriteRenderer.material = TrunslucentShader;
            canBuild = true;
        }
    }
}
