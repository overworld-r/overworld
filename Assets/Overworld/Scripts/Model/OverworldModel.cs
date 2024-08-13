using System.Collections.Generic;
using UnityEngine;

namespace Overworld.Model
{
    [System.Serializable]
    public class OverworldModel
    {
        public Camera UICamera;
        public GameObject Backpack;
        public List<GameObject> ItemPrefabs = new List<GameObject>();
    }
}
