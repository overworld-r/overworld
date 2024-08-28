using Overworld.Core;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    using Models;

    [RequireComponent(typeof(BoxCollider2D))]
    public class Placeable : MonoBehaviour, IClickable
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [SerializeField]
        private bool Duplicatable = default!;

        private Material TrunslucentShader = default!;
        private Material HighlightRedShader = default!;
        private SpriteRenderer spriteRenderer = default!;

        private PlayerPointer playerPointer = default!;

        // [NonSerialized]
        public bool canBuild = true;

        void Start()
        {
            TrunslucentShader = new Material(Shader.Find("unlit/Translucent"));
            HighlightRedShader = new Material(Shader.Find("Unlit/HighlightRed"));
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerPointer = overworldModel.Pointer.GetComponent<PlayerPointer>();
        }

        void IClickable.OnClick(IOption<GameObject> itemPrefab)
        {
            if (
                playerPointer?.locationStatus.value != LocationStatus.Location.World
                || !canBuild
                || playerPointer.holdingItem.IsEmpty
            )
            {
                return;
            }

            var newObject = Instantiate(itemPrefab.Value);
            newObject.name = this.gameObject.name;
            var mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            newObject.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
            newObject.transform.rotation = this.gameObject.transform.rotation;

            newObject.OptGetComponent<Collider2D>(some: collider =>
            {
                collider.isTrigger = playerPointer.holdingItem.IsEmpty;
            });

            newObject.OptGetComponent<Rigidbody2D>(none: () =>
            {
                if (playerPointer.holdingItem.IsEmpty)
                    newObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            });

            newObject.OptGetComponent<SpriteRenderer>(some: renderer =>
            {
                // renderer.material = itemBase.isHolding
                //     ? TrunslucentShader
                //     : new Material(Shader.Find("Sprites/Default"));
            });

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

        public void OnTriggerEnter2D(Collider2D other)
        {
            playerPointer.holdingItem.Match(some: item =>
            {
                if (
                    playerPointer.locationStatus.value != LocationStatus.Location.World
                    || item != this.gameObject
                    || !canBuild
                )
                {
                    return;
                }

                spriteRenderer.material = HighlightRedShader;
                canBuild = false;
            });
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerEnter2D(other);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            playerPointer.holdingItem.Match(some: item =>
            {
                if (
                    playerPointer.locationStatus.value != LocationStatus.Location.World
                    || item != this.gameObject
                    || canBuild
                )
                {
                    return;
                }

                spriteRenderer.material = TrunslucentShader;
                canBuild = true;
            });
        }
    }
}
