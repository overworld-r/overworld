using Overworld.Core;
using Overworld.Features.Player;
using Overworld.Mechanics;
using Overworld.Models;
using UnityEngine;

namespace Overworld.Features.Resource
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Attractable))]
    public class Resource : MonoBehaviour
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        void Update()
        {
            var attractable = this.GetComponent<Attractable>();
            attractable.Attract(0.2f, 5f, overworldModel.Player.transform.position);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == overworldModel.Player)
            {
                overworldModel
                    .Player.GetComponent<PlayerController>()
                    .PickupResource(this.gameObject);
            }
        }
    }
}
