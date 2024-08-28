using Overworld.Core;
using Overworld.Types;

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
            player.Match(p => p.isWallJumping = false);
        }
    }
}
