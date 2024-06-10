using UnityEngine;

namespace Overworld.Item
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

            Destroy(this.gameObject);
            var newGameObject = Instantiate(this.gameObject);

            ItemGeneration itemGeneration = newGameObject.GetComponent<ItemGeneration>();
            itemGeneration.existanceStatus = ExistanceStatus.Physical;

            Destroy(newGameObject.GetComponent<Rigidbody2D>());
            newGameObject.GetComponent<Collider2D>().enabled = true;
            newGameObject.GetComponent<Collider2D>().isTrigger = false;
            newGameObject.GetComponent<SpriteRenderer>().material.shader = Shader.Find(
                "Sprites/Default"
            );
            return newGameObject;
        }

        public GameObject GenerateAsGhost()
        {
            Destroy(this.gameObject);
            var newGameObject = Instantiate(this.gameObject);

            ItemGeneration itemGeneration = newGameObject.GetComponent<ItemGeneration>();
            itemGeneration.existanceStatus = ExistanceStatus.Ghost;

            newGameObject.AddComponent<Rigidbody2D>();
            newGameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            newGameObject.GetComponent<Collider2D>().enabled = true;
            newGameObject.GetComponent<Collider2D>().isTrigger = true;
            newGameObject.GetComponent<SpriteRenderer>().material.shader = TrunslucentShader;
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
