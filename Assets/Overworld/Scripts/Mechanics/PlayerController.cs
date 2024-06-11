using Overworld.Player;
using UnityEngine;
using static Overworld.Core.Simulation;

namespace Overworld.Mechanics
{
    public class PlayerController : KinematicObject
    {
        public float maxSpeed = 8;
        public float jumpTakeOffSpeed = 15;

        public JumpState jumpState = JumpState.Grounded;

        public bool controlEnabled = true;
        bool isJump = false;
        bool stopJump = false;

        public float inputHorizontal { get; private set; } = 0;
        private Vector2 currentVelocity = Vector2.zero;

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                inputHorizontal = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                {
                    jumpState = JumpState.PrepareToJump;
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                inputHorizontal = 0;
            }

            UpdateJumpState();
            base.Update();
        }

        protected override void FixedUpdate()
        {
            Move();
        }

        void UpdateJumpState()
        {
            isJump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    isJump = true;
                    Jump();
                    break;
                case JumpState.Jumping:
                    if (!isGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (isGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        void Move()
        {
            Vector2 moveVector = Vector2.zero;

            if (!isGrounded)
            {
                moveVector = new Vector2(inputHorizontal * maxSpeed * 0.9f, body.velocity.y);
            }
            else
            {
                moveVector = new Vector2(inputHorizontal * maxSpeed, body.velocity.y);
            }
            body.velocity = Vector2.SmoothDamp(
                body.velocity,
                moveVector,
                ref currentVelocity,
                0.01f
            );
        }

        void Jump()
        {
            Vector2 jumpVector = new Vector2(0.0f, jumpTakeOffSpeed);
            body.AddForce(jumpVector, ForceMode2D.Impulse);
        }

        protected override void ComputeVelocity()
        {
            if (stopJump)
            {
                stopJump = false;
                if (body.velocity.y > 0)
                {
                    body.AddForce(Vector2.up * -body.velocity.y * 0.5f, ForceMode2D.Impulse);
                }
            }
        }
    }
}
