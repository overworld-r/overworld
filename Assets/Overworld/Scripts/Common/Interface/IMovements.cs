using UnityEngine;

public class IMovements
{
    public interface IJump
    {
        float jumpStrength { get; }
        float jumpTimer { get; }
        bool isJump { get; }
        bool isGrounded { get; }

        KeyCode jumpKey { get; }

        void Jump();
    }

    public interface IWalk
    {
        float walkSpeed { get; }
        float inputHorizontal { get; }
        void Move(float inputHorizontal);
    }
}
