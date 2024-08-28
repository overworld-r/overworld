using System.Collections.Generic;
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
        public PlayerStatus PlayerStatus = new PlayerStatus();

        public List<GameObject> ItemPrefabs = new List<GameObject>();
        public List<GameObject> ResourcePrefabs = new List<GameObject>();

        public enum LocationStatus
        {
            World,
            Bag,
        }
    }
}
