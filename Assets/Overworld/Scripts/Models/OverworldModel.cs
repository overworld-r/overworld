using UnityEngine;

namespace Overworld.Models
{
    [System.Serializable]
    public class OverworldModel
    {
        public Camera UICamera = default!;
        public GameObject Backpack = default!;
        public string CanvasObjectName = "Canvas";
        public GameObject Pointer = default!;

        public enum LocationStatus
        {
            World,
            Bag,
        }
    }
}
