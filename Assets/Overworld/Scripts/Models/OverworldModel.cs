using UnityEngine;

namespace Overworld.Models
{
    [System.Serializable]
    public class OverworldModel
    {
        public Camera UICamera = default!;
        public GameObject Backpack = default!;
        public GameObject Canvas = default!;
        public GameObject Pointer = default!;
        public GameObject Player = default!;

        public enum LocationStatus
        {
            World,
            Bag,
        }
    }
}
