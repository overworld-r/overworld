using UnityEngine;

namespace Overworld.Features.CustomCollision
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CustomCollision : MonoBehaviour
    {
        [SerializeField]
        private string ID = "";

        void OnTriggerEnter2D(Collider2D collider)
        {
            ICustomCollision customCollision =
                this.transform.parent.GetComponent<ICustomCollision>();
            customCollision.OnCustomCollisionEnter(ID, collider);
        }

        void OnTriggerStay2D(Collider2D collider)
        {
            ICustomCollision customCollision =
                this.transform.parent.GetComponent<ICustomCollision>();
            customCollision.OnCustomCollisionStay(ID, collider);
        }
    }

    public interface ICustomCollision
    {
        void OnCustomCollisionEnter(string ID, Collider2D collider) { }
        void OnCustomCollisionStay(string ID, Collider2D collider) { }
    }
}
