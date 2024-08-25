using Overworld.Core;

namespace Overworld.Features.Player
{
    public class PlayerStopJump : Simulation.Event<PlayerStopJump>
    {
        public PlayerController? player;

        public override void Execute() { }
    }
}
