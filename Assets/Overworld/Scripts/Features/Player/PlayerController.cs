using Overworld.Core;
using Overworld.Features.Resource.Models;
using Overworld.Mechanics;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;
using static Overworld.Core.Simulation;

namespace Overworld.Features.Player
{
    public class PlayerController : KinematicObject
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        public float maxSpeed = 8;
        public float jumpTakeOffSpeed = 15;
        public float wallJumpTakeOffSpeed = 15;
        public bool isWallJumping = false;

        public JumpState jumpState = JumpState.Grounded;

        public bool controlEnabled = true;
        bool stopJump = false;

        public float inputHorizontal { get; private set; } = 0;
        private Vector2 currentVelocity = Vector2.zero;
        private Vector2 ExternalForce = Vector2.zero;

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            PrepareToWallJump,
            Jumping,
            InFlight,
            Landed,
            WallSliding,
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
                else if (jumpState == JumpState.WallSliding && Input.GetButtonDown("Jump"))
                {
                    jumpState = JumpState.PrepareToWallJump;
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
            if (!isWallJumping)
            {
                Move();
            }
            if (jumpState == JumpState.WallSliding)
            {
                WallSlide();
            }
        }

        void UpdateJumpState()
        {
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    Jump();
                    break;
                case JumpState.PrepareToWallJump:
                    jumpState = JumpState.InFlight;
                    WallJump();
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
                    if (wallSlideState != WallSlideState.None)
                    {
                        jumpState = JumpState.WallSliding;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
                case JumpState.WallSliding:
                    if (isGrounded)
                    {
                        jumpState = JumpState.Grounded;
                    }
                    else if (wallSlideState == WallSlideState.None)
                    {
                        jumpState = JumpState.InFlight;
                    }
                    break;
            }
        }

        void Criming() { }

        void Move()
        {
            Vector2 moveVector = Vector2.zero;
            if (isGrounded)
            {
                var internalForce = new Vector2(inputHorizontal * maxSpeed, body.velocity.y);
                moveVector =
                    internalForce + new Vector2(ExternalForce.x * 6.0f, ExternalForce.y * 1.8f);
            }
            else
            {
                var internalForce = new Vector2(inputHorizontal * maxSpeed * 0.3f, body.velocity.y);
                moveVector = internalForce + new Vector2(ExternalForce.x * 2.0f, 0.0f);
            }

            ExternalForce *= 0.8f;

            if (isGrounded)
            {
                body.velocity = Vector2.SmoothDamp(
                    body.velocity,
                    moveVector,
                    ref currentVelocity,
                    0.01f
                );
            }
            else
            {
                body.velocity = moveVector + new Vector2(body.velocity.x * 0.7f, 0);
            }
        }

        public void Push(Vector2 force)
        {
            ExternalForce = force;
        }

        void WallSlide()
        {
            if (body.velocity.y > 0.0f)
            {
                return;
            }
            body.AddForce(new Vector2(0, 0.6f), ForceMode2D.Impulse);
        }

        void Jump()
        {
            Vector2 jumpVector = new Vector2(0.0f, jumpTakeOffSpeed);
            body.AddForce(jumpVector, ForceMode2D.Impulse);
        }

        void WallJump()
        {
            Vector2 wallJumpVector = Vector2.zero;
            if (wallSlideState == WallSlideState.Right)
            {
                wallJumpVector = new Vector2(-wallJumpTakeOffSpeed * 0.7f, wallJumpTakeOffSpeed);
                body.velocity = wallJumpVector;
            }
            else if (wallSlideState == WallSlideState.Left)
            {
                wallJumpVector = new Vector2(wallJumpTakeOffSpeed * 0.7f, wallJumpTakeOffSpeed);
                body.velocity = wallJumpVector;
            }
            body.velocity = wallJumpVector;

            isWallJumping = true;
            Schedule<PlayerStopWallJump>(0.2f).player = this;
        }

        public void PickupResource(GameObject resource)
        {
            var resourceComponent = resource.gameObject.GetComponent<IResourceMetadata>();
            overworldModel
                .PlayerStatus.resourcesAmount.Find(v => v.type == resourceComponent.type)
                .Match(
                    none: () =>
                    {
                        overworldModel.PlayerStatus.resourcesAmount.Add(
                            new ResourcesAmount(resourceComponent.type, 1)
                        );
                    },
                    some: v =>
                    {
                        v.amount += 1;
                        Destroy(resource);
                    }
                );
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