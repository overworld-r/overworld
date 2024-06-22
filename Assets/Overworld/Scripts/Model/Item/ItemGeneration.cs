using UnityEngine;

namespace Overworld.Model
{
    public class ItemGeneration : MonoBehaviour
    {
        public enum ExistanceStatus
        {
            None,
            Ghost,
            Physical,
        }

        public bool canBuild { get; private set; }

        public ExistanceStatus existanceStatus;

        [SerializeField]
        public Shader TrunslucentShader;

        [SerializeField]
        public Shader HighlightRedShader;

        protected virtual void Update() { }

        protected virtual void FixedUpdate() { }

        public GameObject OnClicked()
        {
            switch (existanceStatus)
            {
                case ExistanceStatus.Physical:
                    return GenerateAsGhost();
                case ExistanceStatus.Ghost:
                    return GenerateAsPhysical();
                case ExistanceStatus.None:
                    return null;
            }

            return null;
        }

        public GameObject GenerateAsPhysical()
        {
            if (!canBuild)
            {
                return this.gameObject;
            }

            var newItem = GenerateItem(ExistanceStatus.Physical);

            Destroy(newItem.GetComponent<Rigidbody2D>());
            var collider = newItem.GetComponent<Collider2D>();
            collider.enabled = true;
            collider.isTrigger = false;
            newItem.GetComponent<SpriteRenderer>().material.shader = Shader.Find("Sprites/Default");

            return newItem;
        }

        public GameObject GenerateAsGhost()
        {
            var newItem = GenerateItem(ExistanceStatus.Ghost);

            newItem.AddComponent<Rigidbody2D>().gravityScale = 0.0f;
            var collider = newItem.GetComponent<Collider2D>();
            collider.enabled = true;
            collider.isTrigger = true;
            newItem.GetComponent<SpriteRenderer>().material.shader = TrunslucentShader;

            return newItem;
        }

        private GameObject GenerateItem(ExistanceStatus status)
        {
            Destroy(this.gameObject);
            var newGameObject = Instantiate(this.gameObject);
            var itemGeneration = newGameObject.GetComponent<ItemGeneration>();
            itemGeneration.existanceStatus = status;

            return newGameObject;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (existanceStatus == ExistanceStatus.Ghost && canBuild == true)
            {
                GetComponent<SpriteRenderer>().material.shader = HighlightRedShader;
            }
            canBuild = false;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (existanceStatus == ExistanceStatus.Ghost && canBuild == true)
            {
                GetComponent<SpriteRenderer>().material.shader = HighlightRedShader;
            }
            canBuild = false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (existanceStatus == ExistanceStatus.Ghost && canBuild == false)
            {
                GetComponent<SpriteRenderer>().material.shader = TrunslucentShader;
            }
            canBuild = true;
        }
    }
}
