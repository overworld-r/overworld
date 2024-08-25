using Overworld.Core;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(Item.Models.ItemBase))]
    public class Placeable : MonoBehaviour, IClickable
    {
        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        public bool canBuild = true;
        private Material? TrunslucentShader;
        private Material? HighlightRedShader;
        private SpriteRenderer? spriteRenderer;
        private PlayerPointer? playerPointer;

        Item.Models.ItemBase? itemBase;

        public void Awake()
        {
            itemBase = GetComponent<Item.Models.ItemBase>();
            TrunslucentShader = new Material(Shader.Find("unlit/Translucent"));
            HighlightRedShader = new Material(Shader.Find("Unlit/HighlightRed"));
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerPointer = overworldModel.Pointer?.GetComponent<PlayerPointer>();
        }

        void IClickable.OnClick(GameObject itemPrefab)
        {
            if (itemBase?.locationStatus != OverworldModel.LocationStatus.World || !canBuild)
            {
                return;
            }

            if (playerPointer == null)
            {
                throw new System.Exception("PlayerPointer is null");
            }

            itemBase.isHolding = !itemBase.isHolding;
            var newObject = Instantiate(itemPrefab, this.transform.parent);
            newObject.name = this.gameObject.name;
            newObject.transform.position = this.gameObject.transform.position;
            newObject.transform.rotation = this.gameObject.transform.rotation;

            if (newObject.TryGetComponent<Item.Models.ItemBase>(out var item))
            {
                item.isHolding = itemBase.isHolding;
                item.locationStatus = itemBase.locationStatus;
            }

            if (newObject.TryGetComponent<Collider2D>(out var collider))
            {
                collider.isTrigger = itemBase.isHolding;
            }

            if (itemBase.isHolding && !newObject.TryGetComponent<Rigidbody2D>(out var rigidbody))
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

            if (itemBase.isHolding)
            {
                playerPointer.holdingItem = new Some<GameObject>(newObject);
            }
            else
            {
                playerPointer.holdingItem = new None<GameObject>();
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (itemBase?.locationStatus != OverworldModel.LocationStatus.World)
            {
                return;
            }

            if (!itemBase.isHolding || !canBuild)
            {
                return;
            }
            // spriteRenderer.material = HighlightRedShader;
            canBuild = false;
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (itemBase?.locationStatus != OverworldModel.LocationStatus.World)
            {
                return;
            }
            OnTriggerEnter2D(other);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (itemBase?.locationStatus != OverworldModel.LocationStatus.World)
            {
                return;
            }
            if (!itemBase.isHolding || canBuild)
            {
                return;
            }
            // spriteRenderer.material = TrunslucentShader;
            canBuild = true;
        }
    }
}
