using MyBox;
using Overworld.Core;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    using System.Collections.Generic;
    using Models;
    using Overworld.Features.Resource.Models;

    [RequireComponent(typeof(BoxCollider2D))]
    public class Placeable : MonoBehaviour, IClickable
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [SerializeField]
        private bool Duplicatable = false;

        [ConditionalField(nameof(Duplicatable))]
        public bool UseDuplicateTexture = false;

        [ConditionalField(nameof(UseDuplicateTexture))]
        public Sprite DuplicateTexture = default!;

        [SerializeField]
        public List<ResourceCost> DuplicateCost = new List<ResourceCost>();

        private Material shaderPlaceable = default!;
        private Material shaderNotPlaceable = default!;
        private Material shaderDefault = default!;
        private SpriteRenderer spriteRenderer = default!;

        private PlayerPointer playerPointer = default!;

        // [NonSerialized]
        public bool canBuild { get; private set; } = true;

        void Start()
        {
            shaderPlaceable = new Material(Shader.Find("Unlit/Translucent")).Except(
                "Translucentシェーダーが見つかりません"
            );
            shaderNotPlaceable = new Material(Shader.Find("Unlit/HighlightRed")).Except(
                "HighlightRedシェーダーが見つかりません"
            );
            shaderDefault = new Material(Shader.Find("Sprites/Default")).Except(
                "Defaultシェーダーが見つかりません"
            );

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

            if (
                Duplicatable
                && !Player.Consume.UseResource(DuplicateCost)
                && DuplicateCost.Count != 0
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

            if (UseDuplicateTexture)
            {
                newObject.OptGetComponent<SpriteRenderer>(some: renderer =>
                {
                    renderer.sprite = DuplicateTexture;
                });
            }

            if (!Duplicatable)
            {
                Destroy(this.gameObject);

                playerPointer.holdingItem.Match(
                    some: _ =>
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

        public void EnableCanBuilt()
        {
            canBuild = true;
            spriteRenderer.material = shaderPlaceable;
        }

        public void DisableCanBuilt()
        {
            canBuild = false;
            spriteRenderer.material = shaderNotPlaceable;
        }

        public void EffectOn()
        {
            spriteRenderer.material = shaderPlaceable;
        }

        public void EffectOff()
        {
            spriteRenderer.material = shaderDefault;
        }

        void OnTriggerEnter2D(Collider2D other)
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

                DisableCanBuilt();
            });
        }

        void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerEnter2D(other);
        }

        void OnTriggerExit2D(Collider2D other)
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

                EnableCanBuilt();
            });
        }
    }
}
