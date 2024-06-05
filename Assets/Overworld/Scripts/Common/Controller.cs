using UnityEngine;

public interface IJump
{
    float jumpStrength { get; }
    float jumpTimer { get; }
    bool isJump { get; }
    string groundTag { get; }

    KeyCode jumpKey { get; }

    void Jump();
}

public interface IWalk
{
    float walkSpeed { get; }
    float inputHorizontal { get; }
    void Move(float inputHorizontal);
}

public class Controller : MonoBehaviour, IJump, IWalk
{
    public float jumpStrength { get; } = 10.0f; // Increased for better jumping effect
    public float walkSpeed { get; } = 10.0f;

    public float jumpTimer { get; private set; } = 0.0f;
    public float inputHorizontal { get; private set; } = 0.0f;

    public bool isGrounded { get; private set; } = false;
    public bool isJump { get; private set; } = false;

    public string groundTag { get; } = "Ground";
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
                jumpTimer += 0.05f;
        }
    }

    private void FixedUpdate()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        Move(inputHorizontal);
    }

    public void Jump()
    {
        if (isGrounded && Input.GetKeyDown(jumpKey) && !isJump)
        {
            Vector2 jumpVector = new Vector2(0.0f, jumpStrength);
            rb.AddForce(jumpVector, ForceMode2D.Impulse);
            isJump = true;
            jumpTimer = 0.0f;
        }

        if (Input.GetKeyUp(jumpKey) && isJump)
        {
            rb.AddForce(
                new Vector2(0.0f, jumpStrength * -(7.0f - jumpTimer) * 0.1f),
                ForceMode2D.Impulse
            );
        }

        if (Input.GetKey(jumpKey) && isJump)
        {
            if (jumpTimer <= 7.0f)
            {
                jumpTimer += 0.05f;
            }
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
            isJump = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = false;
        }
    }
}