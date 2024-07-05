using Overworld.Core;
using Overworld.Mechanics;
using UnityEngine;

namespace Overworld.Player
{
    /// <summary>
    /// Fired when the player performs a Jump.
    /// </summary>
    /// <typeparam name="PlayerJumped"></typeparam>
    public class PlayerJumped : Simulation.Event<PlayerJumped>
    {
        public PlayerController player;

        public override void Execute()
        {
            // if (player.audioSource && player.jumpAudio)
            //     player.audioSource.PlayOneShot(player.jumpAudio);
        }
    }
}
