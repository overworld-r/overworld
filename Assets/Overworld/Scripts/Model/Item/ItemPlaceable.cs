using UnityEngine;

namespace Overworld.Model
{
    public abstract class ItemPlaceable : IItem
    {
        public bool canBuild { get; private set; } = true;

        private Material TrunslucentShader;
        private Material HighlightRedShader;
        private SpriteRenderer spriteRenderer;

        public void Awake()
        {
            TrunslucentShader = new Material(Shader.Find("unlit/Translucent"));
            HighlightRedShader = new Material(Shader.Find("Unlit/HighlightRed"));
            TryGetComponent<SpriteRenderer>(out spriteRenderer);
            itemLocationStatus = ItemLocationStatus.World;
        }

        protected virtual void Update() { }

        protected virtual void FixedUpdate() { }

        public override GameObject OnClick(GameObject itemPrefab)
        {
            if (!itemPrefab || !canBuild)
            {
                return this.gameObject;
            }

            switch (itemLocationStatus)
            {
                case ItemLocationStatus.World:
                    return TogglePlaceInWorld(itemPrefab);
                case ItemLocationStatus.Bag:
                    return TogglePlaceInBag(itemPrefab);
                default:
                    return this.gameObject;
            }
        }

        GameObject TogglePlaceInBag(GameObject itemPrefab)
        {
            return this.gameObject;
        }

        GameObject TogglePlaceInWorld(GameObject itemPrefab)
        {
            isHolding = !isHolding;
            var newObject = Instantiate(itemPrefab);
            newObject.name = this.gameObject.name;
            newObject.transform.position = this.gameObject.transform.position;
            newObject.transform.rotation = this.gameObject.transform.rotation;

            {
                newObject.TryGetComponent<IItem>(out var item);
                item.isHolding = isHolding;
            }
            {
                newObject.TryGetComponent<Collider2D>(out var collider);
                collider.isTrigger = isHolding;
            }
            {
                newObject.TryGetComponent<SpriteRenderer>(out var renderer);
                renderer.material = isHolding
                    ? TrunslucentShader
                    : new Material(Shader.Find("Sprites/Default"));
            }

            Destroy(this.gameObject);
            return newObject;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!this.isHolding || !canBuild)
            {
                return;
            }
            spriteRenderer.material = HighlightRedShader;
            canBuild = false;
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerEnter2D(other);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (!this.isHolding || canBuild)
            {
                return;
            }
            spriteRenderer.material = TrunslucentShader;
            canBuild = true;
        }
    }
}
