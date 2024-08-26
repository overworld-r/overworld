using UnityEngine;

namespace Overworld.CustomCollision
{
    public class CustomCollision : MonoBehaviour
    {
        [SerializeField]
        private string ID = "";

        void OnTriggerEnter2D(Collider2D collider)
        {
            ICustomCollision customCollision =
                this.transform.parent.GetComponent<ICustomCollision>();
            if (customCollision != null)
            {
                customCollision.OnCustomCollision(ID, collider);
            }
        }
    }

    public interface ICustomCollision
    {
        void OnCustomCollision(string ID, Collider2D collider);
    }
}
