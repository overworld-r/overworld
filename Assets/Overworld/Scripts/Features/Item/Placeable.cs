using UnityEngine;

namespace Overworld.Item
{
    public class Placeable : MonoBehaviour
    {
        public bool canBuild = true;
        private Material TrunslucentShader;
        private Material HighlightRedShader;
        private SpriteRenderer spriteRenderer;
        private ItemBase itemBase;

        public void Awake()
        {
            if (!TryGetComponent<ItemBase>(out var itemBaseComponent))
            {
                return;
            }
            itemBase = itemBaseComponent;

            TrunslucentShader = new Material(Shader.Find("unlit/Translucent"));
            HighlightRedShader = new Material(Shader.Find("Unlit/HighlightRed"));
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public GameObject OnClick(GameObject itemPrefab)
        {
            if (itemBase.locationStatus != ItemBase.LocationStatus.World || !canBuild)
            {
                return this.gameObject;
            }

            itemBase.isHolding = !itemBase.isHolding;
            var newObject = Instantiate(itemPrefab, this.transform.parent);
            newObject.name = this.gameObject.name;
            newObject.transform.position = this.gameObject.transform.position;
            newObject.transform.rotation = this.gameObject.transform.rotation;

            if (newObject.TryGetComponent<ItemBase>(out var item))
            {
                item.isHolding = itemBase.isHolding;
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
                renderer.material = itemBase.isHolding
                    ? TrunslucentShader
                    : new Material(Shader.Find("Sprites/Default"));
            }

            Destroy(this.gameObject);
            return newObject;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (itemBase.locationStatus != ItemBase.LocationStatus.World)
            {
                return;
            }

            if (!itemBase.isHolding || !canBuild)
            {
                return;
            }
            spriteRenderer.material = HighlightRedShader;
            canBuild = false;
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (itemBase.locationStatus != ItemBase.LocationStatus.World)
            {
                return;
            }
            OnTriggerEnter2D(other);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (itemBase.locationStatus != ItemBase.LocationStatus.World)
            {
                return;
            }
            if (!itemBase.isHolding || canBuild)
            {
                return;
            }
            spriteRenderer.material = TrunslucentShader;
            canBuild = true;
        }
    }
}
