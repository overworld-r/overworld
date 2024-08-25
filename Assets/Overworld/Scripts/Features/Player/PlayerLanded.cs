using Overworld.Core;

namespace Overworld.Features.Player
{
    /// <summary>
    /// Fired when the player character lands after being airborne.
    /// </summary>
    /// <typeparam name="PlayerLanded"></typeparam>
    public class PlayerLanded : Simulation.Event<PlayerLanded>
    {
        public PlayerController? player;

        public override void Execute() { }
    }
}
