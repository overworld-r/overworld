using Overworld.Core;
using Overworld.Features.Item.Models;
using UnityEngine;

namespace Overworld.Models
{
    class GameController : MonoBehaviour
    {
        public static GameController? Instance { get; private set; }

        public OverworldModel model = Simulation.GetModel<OverworldModel>();

        void Start()
        {
            var prefabs = Resources.LoadAll<GameObject>("ItemPrefabs");

            foreach (var prefab in prefabs)
            {
                if (!prefab.TryGetComponent<IItemMetadata>(out _))
                {
                    throw new System.Exception("IItemMetadataが実装されていません");
                }
            }
        }

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