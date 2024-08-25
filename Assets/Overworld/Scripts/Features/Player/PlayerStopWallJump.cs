using Overworld.Core;

namespace Overworld.Features.Player
{
    /// <summary>
    /// Fired when the player character lands after being airborne.
    /// </summary>
    /// <typeparam name="PlayerLanded"></typeparam>
    public class PlayerStopWallJump : Simulation.Event<PlayerStopWallJump>
    {
        public PlayerController? player;

        public override void Execute()
        {
            if (player != null)
                player.isWallJumping = false;
        }
    }
}
