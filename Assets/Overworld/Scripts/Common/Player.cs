using UnityEngine;

public class Player : MonoBehaviour
{

    private Movements move = new Movements();

    //�ړ����x
    [Range(1, 20)][SerializeField] private float moveSpeed = 10.0f;
    //�W�����v��
    [Range(1, 20)][SerializeField] private float jumpStrength = 1.0f;
    //�W�����v�L�[�𒷉������Ă�������
    private float jumpTime = 7.0f;
    //�n�ʂ̃^�O
    private string groundTag = "Ground";

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
        
    }

    void Update()
    {

        //�W�����v
        if (isGround && Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            Jump();
        } else if (Input.GetKeyUp(KeyCode.Space) && isJump)
        {
            move.addForce(this.GetComponent<Rigidbody2D>(), new Vector2(0.0f, jumpStrength * -jumpTime * 0.1f));
            jumpTime = 7.0f;
        } else if (Input.GetKey(KeyCode.Space) && isJump)
        {
            if(jumpTime >= 0.0f) jumpTime -= 0.05f;
        }

    }

    private void FixedUpdate()
    {
        // �ړ�
        float inputHorizontal = Input.GetAxis("Horizontal");
        Move(inputHorizontal);
    }

    private void Move(float inputHorizontal)
    {
        Vector2 moveVector = Vector2.zero;
        if (!isGround) moveVector = new Vector2(inputHorizontal * moveSpeed * 0.7f, this.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        else moveVector = new Vector2(inputHorizontal * moveSpeed, this.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        move.changeVelocity(this.GetComponent<Rigidbody2D>(), moveVector, 0.01f);
    }
    private void Jump()
    {
        Vector2 jumpVector = new Vector2(0.0f, jumpStrength);
        move.addForce(this.GetComponent<Rigidbody2D>(), jumpVector);
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