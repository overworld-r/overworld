using UnityEngine;

namespace Overworld.Mechanics
{
    public class KinematicObject : MonoBehaviour
    {
        public bool isGrounded { get; private set; }
        protected Rigidbody2D body;
        protected ContactFilter2D contactFilter;

        public void Teleport(Vector3 position)
        {
            body.position = position;
            body.velocity *= 0;
        }

        protected virtual void OnEnable()
        {
            body = GetComponent<Rigidbody2D>();
        }

        protected virtual void OnDisable() { }

        protected virtual void Start()
        {
            contactFilter.useTriggers = false;
            contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
            contactFilter.useLayerMask = true;
        }

        protected virtual void Update()
        {
            isGrounded = JudgeGrounded();
        }

        protected virtual void FixedUpdate() { }

        protected virtual bool JudgeGrounded()
        {
            int count = body.Cast(Vector2.down, contactFilter, new RaycastHit2D[1], 0.1f);

            return count > 0;
        }
    }
}
