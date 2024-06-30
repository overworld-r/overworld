using Overworld.Core;
using UnityEngine;

namespace Overworld.Mechanics
{
    class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this)
                Instance = null;
        }

        void Update()
        {
            if (Instance == this)
                Simulation.Tick();
        }
    }
}
