using Overworld.Item.Model;
using Overworld.Model;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Item
{
    [RequireComponent(typeof(ItemBase))]
    public class Placeable : MonoBehaviour, IClickable
    {
        public bool canBuild = true;
        private Material? TrunslucentShader;
        private Material? HighlightRedShader;
        private SpriteRenderer? spriteRenderer;

        ItemBase? itemBase;

        public void Awake()
        {
            itemBase = GetComponent<ItemBase>();
            TrunslucentShader = new Material(Shader.Find("unlit/Translucent"));
            HighlightRedShader = new Material(Shader.Find("Unlit/HighlightRed"));
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        IOption<GameObject> IClickable.OnClick(GameObject itemPrefab)
        {
            if (itemBase?.locationStatus != OverworldModel.LocationStatus.World || !canBuild)
            {
                return new Some<GameObject>(this.gameObject);
            }

            itemBase.isHolding = !itemBase.isHolding;

            var newObject = Instantiate(itemPrefab, this.transform.parent);
            newObject.name = this.gameObject.name;
            newObject.transform.position = this.gameObject.transform.position;
            newObject.transform.rotation = this.gameObject.transform.rotation;

            if (newObject.TryGetComponent<ItemBase>(out var item))
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

            return itemBase.isHolding ? new Some<GameObject>(newObject) : new None<GameObject>();
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
