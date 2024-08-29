using UnityEngine;

namespace Overworld.Mechanics
{
    public class Attractable : MonoBehaviour
    {
        public void Attract(float strength, float radius, Vector3 pos)
        {
            Vector3 direction = pos - transform.position;
            float distance = direction.magnitude;

            if (distance < radius)
            {
                var force = direction.normalized * strength * (1 - distance / radius);
                GetComponent<Rigidbody2D>().AddForce(force);
            }
        }
    }
}
