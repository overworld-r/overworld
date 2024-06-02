using UnityEngine;

public class Player : MonoBehaviour
{

    private Movements move = new Movements();

    //Movement Speed
    [Range(1, 20)][SerializeField] private float moveSpeed = 10.0f;
    //Jump Strength
    [Range(1, 20)][SerializeField] private float jumpStrength = 1.0f;
    //Time spent holding down the space key.
    private float jumpTime = 0.0f;
    //Ground Tag
    private string groundTag = "Ground";
    //Rigidbody2D
    private Rigidbody2D rb;

    public bool isGround
    {
        get;
        private set;
    }
    public bool isJump
    {
        get;
        private set;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        //Jump
        if (isGround && Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            Jump();
            jumpTime = 0.0f;
        } else if (Input.GetKeyUp(KeyCode.Space) && isJump)
        {
            move.addForce(rb, new Vector2(0.0f, jumpStrength * -(7.0f - jumpTime) * 0.1f));
        } else if (Input.GetKey(KeyCode.Space) && isJump)
        {
            if(jumpTime <= 7.0f) jumpTime += 0.05f;
        }

    }

    private void FixedUpdate()
    {
        //Move
        float inputHorizontal = Input.GetAxis("Horizontal");
        Move(inputHorizontal);
    }

    private void Move(float inputHorizontal)
    {
        Vector2 moveVector = Vector2.zero;
        if (!isGround) moveVector = new Vector2(inputHorizontal * moveSpeed * 0.7f, rb.velocity.y);
        else moveVector = new Vector2(inputHorizontal * moveSpeed, rb.velocity.y);
        move.changeVelocity(rb, moveVector, 0.01f);
    }
    private void Jump()
    {
        Vector2 jumpVector = new Vector2(0.0f, jumpStrength);
        move.addForce(rb, jumpVector);
        isJump = true;
    }

    private void onGroundEnter()
    {
        isGround = true;
        isJump = false;
    }

    private void onGroundEStay()
    {
        isGround = true;
        isJump = false;
    }


    private void onGroundExit()
    {
        isGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == groundTag)
        {
            onGroundEnter();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == groundTag)
        {
            onGroundEStay();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == groundTag)
        {
            onGroundExit();
        }
    }
}