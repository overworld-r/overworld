using UnityEngine;

public class JudgeGrounded : MonoBehaviour
{
    private Controller controller;

    void Start()
    {
        controller = GetComponentInParent<Controller>();
    }

    public void OnTriggerEnter2D()
    {
        controller.OnGroundedEnter();
    }

    public void OnTriggerStay2D()
    {
        controller.OnGroundedStay();
    }

    public void OnTriggerExit2D()
    {
        controller.OnGroundedExit();
    }
}
