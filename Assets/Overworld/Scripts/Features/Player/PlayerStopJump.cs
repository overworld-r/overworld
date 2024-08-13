using Overworld.Core;
using Overworld.Mechanics;

namespace Overworld.Player
{
    public class PlayerStopJump : Simulation.Event<PlayerStopJump>
    {
        public PlayerController player;

        public override void Execute() { }
    }
}
