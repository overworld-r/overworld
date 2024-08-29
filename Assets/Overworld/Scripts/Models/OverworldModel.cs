using System.Collections.Generic;
using UnityEngine;

namespace Overworld.Models
{
    [System.Serializable]
    public class BackpackContents
    {
        public GameObject Self = default!;
        public GameObject Inventory = default!;
        public GameObject StatusPanel = default!;
    }

    [System.Serializable]
    public class Backpack
    {
        public GameObject Self = default!;
        public BackpackContents Contents = default!;
    }

    [System.Serializable]
    public class OverworldModel
    {
        public Camera UICamera = default!;
        public Backpack Backpack = default!;

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
