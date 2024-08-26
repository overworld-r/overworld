using Overworld.Mechanics;
using Overworld.Player;
using Overworld.Types;
using UnityEngine;
using static Overworld.Core.Simulation;

namespace Overworld.Features.Player
{
    public class PlayerController : KinematicObject
    {
        public float maxSpeed = 8;
        public float jumpTakeOffSpeed = 15;
        public float wallJumpTakeOffSpeed = 15;
        public bool isWallJumping = false;
        public bool isPushing = false;

        public JumpState jumpState = JumpState.Grounded;

        public bool controlEnabled = true;
        bool stopJump = false;

        public float inputHorizontal { get; private set; } = 0;
        private Vector2 currentVelocity = Vector2.zero;

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
            if (!isWallJumping && !isPushing)
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

        void Move()
        {
            Vector2 moveVector = Vector2.zero;
            Rigidbody2D _body = body.Unwrap();
            if (!isGrounded)
            {
                moveVector = new Vector2(inputHorizontal * maxSpeed * 0.3f, _body.velocity.y);
            }
            else
            {
                moveVector = new Vector2(inputHorizontal * maxSpeed, _body.velocity.y);
            }

            if (isGrounded)
            {
                _body.velocity = Vector2.SmoothDamp(
                    _body.velocity,
                    moveVector,
                    ref currentVelocity,
                    0.01f
                );
            }
            else
            {
                _body.velocity = moveVector + new Vector2(_body.velocity.x * 0.7f, 0);
            }
        }

        public void Push(Vector2 force)
        {
            gameObject.GetComponent<Rigidbody2D>().Unwrap().AddForce(force, ForceMode2D.Impulse);
            isPushing = true;
            Schedule<PlayerPushed>(0.3f).player = this;
        }

        void WallSlide()
        {
            Rigidbody2D _body = body.Unwrap();
            if (_body.velocity.y > 0.0f)
            {
                return;
            }
            _body.AddForce(new Vector2(0, 0.6f), ForceMode2D.Impulse);
        }

        void Jump()
        {
            Vector2 jumpVector = new Vector2(0.0f, jumpTakeOffSpeed);
            Rigidbody2D _body = body.Unwrap();
            _body.AddForce(jumpVector, ForceMode2D.Impulse);
        }

        void WallJump()
        {
            Vector2 wallJumpVector = Vector2.zero;
            Rigidbody2D _body = body.Unwrap();
            if (wallSlideState == WallSlideState.Right)
            {
                wallJumpVector = new Vector2(-wallJumpTakeOffSpeed * 0.7f, wallJumpTakeOffSpeed);
                _body.velocity = wallJumpVector;
            }
            else if (wallSlideState == WallSlideState.Left)
            {
                wallJumpVector = new Vector2(wallJumpTakeOffSpeed * 0.7f, wallJumpTakeOffSpeed);
                _body.velocity = wallJumpVector;
            }
            _body.velocity = wallJumpVector;

            isWallJumping = true;
            Schedule<PlayerStopWallJump>(0.2f).player = this;
        }

        protected override void ComputeVelocity()
        {
            Rigidbody2D _body = body.Unwrap();

            if (stopJump)
            {
                stopJump = false;
                if (_body.velocity.y > 0)
                {
                    _body.AddForce(Vector2.up * -_body.velocity.y * 0.5f, ForceMode2D.Impulse);
                }
            }
        }
    }
}
