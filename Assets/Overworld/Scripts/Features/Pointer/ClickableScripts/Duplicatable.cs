using Overworld.Core;
using Overworld.Models;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    using Models;

    [RequireComponent(typeof(BoxCollider2D))]
    public class Duplicatable : MonoBehaviour, IClickable
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        private Material? TrunslucentShader;
        private Material? HighlightRedShader;
        private SpriteRenderer? spriteRenderer;

        private PlayerPointer? playerPointer;

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
            if (
                playerPointer?.locationStatus.value != LocationStatus.Location.World
                || !canBuild
                || playerPointer.holdingItem.IsEmpty
            )
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
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            playerPointer?.holdingItem.Match(some: item =>
            {
                if (
                    playerPointer.locationStatus.value != LocationStatus.Location.World
                    || item != this.gameObject
                    || !canBuild
                )
                {
                    return;
                }

                // spriteRenderer.material = HighlightRedShader;
                canBuild = false;
            });
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerEnter2D(other);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            playerPointer?.holdingItem.Match(some: item =>
            {
                if (
                    playerPointer.locationStatus.value != LocationStatus.Location.World
                    || item != this.gameObject
                    || canBuild
                )
                {
                    return;
                }

                // spriteRenderer.material = TrunslucentShader;
                canBuild = true;
            });
        }
    }
}
