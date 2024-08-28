using Overworld.Features.Player;
using Overworld.Mechanics;
using UnityEngine;

namespace Overworld.Features.Resource
{
    public class Resource : MonoBehaviour
    {
        private GameObject player = default!;

        void Start()
        {
            player = GameObject.Find("Player");
        }

        void Update()
        {
            var attractable = this.GetComponent<Attractable>();
            attractable.Attract(0.2f, 5f, player.transform.position);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == player)
            {
                player.GetComponent<PlayerController>().PickupResource(this.gameObject);
            }
        }
    }
}
