using System.Collections.Generic;
using UnityEngine;

namespace Overworld.Model
{
    [System.Serializable]
    public class OverworldModel
    {
        public Camera? UICamera;
        public GameObject? Backpack;
        public string CanvasObjectName = "Canvas";
        public LocationStatus cursorLocationStatus = LocationStatus.World;
        public List<GameObject> ItemPrefabs = new List<GameObject>();

        public enum LocationStatus
        {
            World,
            Bag,
        }
    }
}
