using Overworld.Core;
using UnityEngine;

namespace Overworld.Model
{
    class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public OverworldModel model = Simulation.GetModel<OverworldModel>();

        public void OnEnable()
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
