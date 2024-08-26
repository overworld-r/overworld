using Overworld.Core;

namespace Overworld.Features.Player
{
    /// <summary>
    /// Fired when the player character lands after being airborne.
    /// </summary>
    /// <typeparam name="PlayerPushed"></typeparam>
    public class PlayerPushed : Simulation.Event<PlayerPushed>
    {
        public PlayerController player;

        public override void Execute()
        {
            player.isPushing = false;
        }
    }
}
