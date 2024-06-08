using UnityEngine;
using static IMovements;

public class Controller : MonoBehaviour, IJump, IWalk
{
    public float jumpStrength { get; } = 10.0f;
    public float walkSpeed { get; } = 5.0f;

    public float jumpTimer { get; private set; } = 0.0f;
    public float inputHorizontal { get; private set; } = 0.0f;

    public bool isJump { get; private set; } = false;
    public bool isGrounded { get; private set; } = false;

    public KeyCode jumpKey { get; } = KeyCode.Space;

    private Rigidbody2D rb;
    private Vector2 currentVelocity = Vector2.zero;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool isKeyDownOnGround = isGrounded && Input.GetKeyDown(jumpKey) && !isJump;
        bool isKeyUpWhileJumping = Input.GetKeyUp(jumpKey) && isJump;
        bool isKeyStayDownWhileJumping = Input.GetKey(jumpKey) && isJump;

        if (isKeyDownOnGround)
        {
            Jump();
            jumpTimer = 0.0f;
        }
        else if (isKeyUpWhileJumping)
        {
            rb.AddForce(
                new Vector2(0.0f, jumpStrength * -(7.0f - jumpTimer) * 0.1f),
                ForceMode2D.Impulse
            );
        }
        else if (isKeyStayDownWhileJumping)
        {
            if (jumpTimer <= 7.0f)
            {
                jumpTimer += Time.deltaTime * 20.0f;
            }
        }
    }

    private void FixedUpdate()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        Move(inputHorizontal);
    }

    public void Jump()
    {
        Vector2 jumpVector = new Vector2(0.0f, jumpStrength);
        rb.AddForce(jumpVector, ForceMode2D.Impulse);
        isJump = true;
    }

    public void Move(float inputHorizontal)
    {
        Vector2 moveVector = Vector2.zero;

        if (!isGrounded)
        {
            moveVector = new Vector2(inputHorizontal * walkSpeed * 0.7f, rb.velocity.y);
        }
        else
        {
            moveVector = new Vector2(inputHorizontal * walkSpeed, rb.velocity.y);
        }
        rb.velocity = Vector2.SmoothDamp(rb.velocity, moveVector, ref currentVelocity, 0.01f);
    }

    public void OnGroundedEnter()
    {
        isGrounded = true;
        isJump = false;
    }

    public void OnGroundedStay()
    {
        isGrounded = true;
        isJump = false;
    }

    public void OnGroundedExit()
    {
        isGrounded = false;
    }
}
