using Overworld.Core;
using Overworld.Mechanics;
using UnityEngine;

namespace Overworld.Player
{
    /// <summary>
    /// Fired when the player character lands after being airborne.
    /// </summary>
    /// <typeparam name="PlayerLanded"></typeparam>
    public class PlayerStopWallJump : Simulation.Event<PlayerStopWallJump>
    {
        public PlayerController player;

        public override void Execute()
        {
            player.isWallJumping = false;
        }
    }
}