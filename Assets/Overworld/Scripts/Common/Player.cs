using UnityEngine;

public class Player : MonoBehaviour
{
    private Controller controll;
    public bool isGrounded { get; private set; }

    public void start()
    {
        controll = GetComponent<Controller>();
    }

    public void update()
    {
        isGrounded = controll.isGrounded;
    }
}
