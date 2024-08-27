using Overworld.Features.Player;
using UnityEngine;

namespace Overworld.Mechanics.Types
{
    public static class PushExtension
    {
        public static void Push(this Rigidbody2D rigidbody, Vector2 force)
        {
            if (rigidbody.gameObject.CompareTag("Player"))
            {
                rigidbody.gameObject.GetComponent<PlayerController>().Push(force);
            }
            else
            {
                rigidbody.AddForce(force);
            }
        }
    }
}
