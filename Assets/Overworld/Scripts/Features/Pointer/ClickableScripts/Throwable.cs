using UnityEngine;

namespace Overworld.Features.Pointer
{
    public class Throwable
    {
        void Start() { }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnMouseDown();
            }
        }

        void OnMouseDown() { }
    }
}
