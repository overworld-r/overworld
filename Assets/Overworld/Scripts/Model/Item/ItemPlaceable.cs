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
            spriteRenderer = GetComponent<SpriteRenderer>();
            itemLocationStatus = ItemLocationStatus.World;
        }

        protected virtual void Update() { }

        protected virtual void FixedUpdate() { }

        public override GameObject OnClick(GameObject itemPrefab, Transform parent)
        {
            if (!itemPrefab || !canBuild)
            {
                return this.gameObject;
            }

            switch (itemLocationStatus)
            {
                case ItemLocationStatus.World:
                    return TogglePlaceInWorld(itemPrefab, parent);
                case ItemLocationStatus.Bag:
                    return TogglePlaceInBag(itemPrefab, parent);
                default:
                    return this.gameObject;
            }
        }

        public override GameObject ChangeLocationStatus(
            GameObject itemPrefab,
            Transform parent,
            ItemLocationStatus status
        )
        {
            var newObject = Instantiate(itemPrefab, parent);
            newObject.name = this.gameObject.name;
            newObject.transform.position = this.gameObject.transform.position;
            newObject.transform.rotation = this.gameObject.transform.rotation;

            if (newObject.TryGetComponent<IItem>(out var item))
            {
                item.isHolding = isHolding;
                item.itemLocationStatus = status;
            }

            Destroy(this.gameObject);
            return newObject;
        }

        GameObject TogglePlaceInBag(GameObject itemPrefab, Transform parent)
        {
            var newObject = Instantiate(itemPrefab, parent);
            newObject.name = this.gameObject.name;
            newObject.transform.position = this.gameObject.transform.position;
            newObject.transform.rotation = this.gameObject.transform.rotation;

            if (newObject.TryGetComponent<IItem>(out var item))
            {
                item.isHolding = isHolding;
            }

            Destroy(this.gameObject);
            return newObject;
        }

        GameObject TogglePlaceInWorld(GameObject itemPrefab, Transform parent)
        {
            isHolding = !isHolding;
            var newObject = Instantiate(itemPrefab, parent);
            newObject.name = this.gameObject.name;
            newObject.transform.position = this.gameObject.transform.position;
            newObject.transform.rotation = this.gameObject.transform.rotation;

            if (newObject.TryGetComponent<IItem>(out var item))
            {
                item.isHolding = isHolding;
            }

            if (newObject.TryGetComponent<Collider2D>(out var collider))
            {
                collider.isTrigger = isHolding;
            }

            if (isHolding && !newObject.TryGetComponent<Rigidbody2D>(out var rigidbody))
            {
                newObject.AddComponent<Rigidbody2D>();
            }

            if (newObject.TryGetComponent<SpriteRenderer>(out var renderer))
            {
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
