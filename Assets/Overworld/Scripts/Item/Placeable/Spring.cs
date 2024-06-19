using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
        public float bounceForce = 10f;  // ���˂�͂̑傫��

       void OnTriggerEnter2D(Collider2D collision)
    {
            // �v���C���[���o�l�ɐG�ꂽ�Ƃ�
            if (collision.gameObject.tag == "Player")
            {
                Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRigidbody != null)
                {
                    // �v���C���[��y�����ɗ͂�������
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, bounceForce);
                }
            }
        }
    

    // Update is called once per frame
    void Update()
    {
       
    }
}
