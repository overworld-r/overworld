using System.Collections.Generic;
using UnityEngine;

namespace Overworld.Models
{
    [System.Serializable]
    public class OverworldModel
    {
        public Camera? UICamera;
        public GameObject? Backpack;
        public string CanvasObjectName = "Canvas";
        public GameObject? Pointer;
        public List<GameObject> ItemPrefabs = new List<GameObject>();

        public enum LocationStatus
        {
            World,
            Bag,
        }
    }
}