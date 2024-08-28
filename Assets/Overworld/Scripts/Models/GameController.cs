using Overworld.Core;
using Overworld.Features.Item.Models;
using UnityEngine;

namespace Overworld.Models
{
    using Overworld.Features.Resource.Models;
    using Types;

    class GameController : MonoBehaviour
    {
        public static GameController? Instance { get; private set; }

        public OverworldModel model = Simulation.GetModel<OverworldModel>();

        void Reset()
        {
            model.UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
            model.Backpack = GameObject.Find("Backpack");
            model.Canvas = model.Backpack.gameObject.transform.Find("Canvas").gameObject;
            model.Pointer = GameObject.Find("Pointer");
            model.Player = GameObject.Find("Player");
        }

        void Start()
        {
            CheckIItemMetadata();
            LoadResources();
            DestroyRigidbodyOfCanvasItem();
        }

        private void DestroyRigidbodyOfCanvasItem()
        {
            foreach (var rb in model.Canvas.GetComponentsInChildren<Rigidbody2D>())
            {
                Destroy(rb);
            }
        }

        private void CheckIItemMetadata()
        {
            foreach (var prefab in Resources.LoadAll<GameObject>("ItemPrefabs"))
            {
                prefab.OptGetComponent<IItemMetadata>(
                    some: _ =>
                    {
                        model.ItemPrefabs.Add(prefab);
                    },
                    none: () =>
                    {
                        throw new System.Exception("ItemにIItemMetadataが実装されていません");
                    }
                );
            }
        }

        private void LoadResources()
        {
            foreach (var prefab in Resources.LoadAll<GameObject>("GameResourcePrefabs"))
            {
                prefab.OptGetComponent<IResourceMetadata>(
                    some: component =>
                    {
                        model.ResourcePrefabs.Add(prefab);
                    },
                    none: () =>
                    {
                        throw new System.Exception(
                            $"ResourceにIResourceが実装されていません: {prefab}"
                        );
                    }
                );
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
