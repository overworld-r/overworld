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

        public override GameObject OnClick(GameObject itemPrefab)
        {
            if (!itemPrefab || !canBuild)
            {
                return this.gameObject;
            }

            switch (itemLocationStatus)
            {
                case ItemLocationStatus.World:
                    return ToggleHoldingStatusInWorld(itemPrefab);
                case ItemLocationStatus.Bag:
                    return ToggleHoldingStatusInBag();
                default:
                    return this.gameObject;
            }
        }

        public override void ChangeLocationStatus(
            GameObject item,
            Transform parent,
            ItemLocationStatus status
        )
        {
            item.transform.parent = parent.transform;

            if (item.TryGetComponent<SpriteRenderer>(out var itemRenderer))
            {
                itemRenderer.sortingOrder = 100;
            }

            if (item.TryGetComponent<IItem>(out var itemComponent))
            {
                itemComponent.itemLocationStatus = status;
            }
        }

        GameObject ToggleHoldingStatusInBag()
        {
            this.isHolding = !this.isHolding;
            Destroy(GetComponent<Rigidbody2D>());
            return this.gameObject;
        }

        GameObject ToggleHoldingStatusInWorld(GameObject itemPrefab)
        {
            isHolding = !isHolding;
            var newObject = Instantiate(itemPrefab, this.transform.parent);
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
            if (this.itemLocationStatus != ItemLocationStatus.World)
            {
                return;
            }

            if (!this.isHolding || !canBuild)
            {
                return;
            }
            spriteRenderer.material = HighlightRedShader;
            canBuild = false;
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (this.itemLocationStatus != ItemLocationStatus.World)
            {
                return;
            }
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
