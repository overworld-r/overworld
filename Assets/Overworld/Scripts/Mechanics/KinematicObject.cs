using UnityEngine;

namespace Overworld.Mechanics
{
    public class KinematicObject : MonoBehaviour
    {
        public bool isGrounded { get; private set; }

        public enum WallSlideState
        {
            Left,
            Right,
            None
        }

        public WallSlideState wallSlideState = WallSlideState.None;

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
            wallSlideState = JudgeWallSliding();
            ComputeVelocity();
        }

        protected virtual void FixedUpdate() { }

        protected virtual void ComputeVelocity() { }

        protected virtual bool JudgeGrounded()
        {
            int count = body.Cast(Vector2.down, contactFilter, new RaycastHit2D[1], 0.1f);

            return count > 0;
        }

        protected virtual WallSlideState JudgeWallSliding()
        {
            int countRight = body.Cast(Vector2.right, contactFilter, new RaycastHit2D[1], 0.2f);
            int countLeft = body.Cast(Vector2.left, contactFilter, new RaycastHit2D[1], 0.2f);

            if (countRight > 0)
            {
                return WallSlideState.Right;
            }
            else if (countLeft > 0)
            {
                return WallSlideState.Left;
            }
            else
            {
                return WallSlideState.None;
            }
        }
    }
}
