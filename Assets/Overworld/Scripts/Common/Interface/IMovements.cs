using UnityEngine;

class IMovements
{
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
}
