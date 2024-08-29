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
            GameObject
                .Find("UICamera")
                .OptGetComponent<Camera>(
                    some: v => model.UICamera = v,
                    none: () =>
                    {
                        Debug.Log("UICameraが見つかりませんでした");
                    }
                );
            GameObject
                .Find("Backpack")
                .Match(
                    some: v => model.Backpack.Self = v,
                    none: () =>
                    {
                        Debug.Log("Backpackが見つかりませんでした");
                    }
                );
            model
                .Backpack.Self.gameObject.transform.Find("Contents")
                .Match(
                    some: v => model.Backpack.Contents.Self = v.gameObject,
                    none: () =>
                    {
                        Debug.Log("Backpack/Contentsが見つかりませんでした");
                    }
                );

            model
                .Backpack.Contents.Self.gameObject.transform.Find("Inventory")
                .Match(
                    some: v => model.Backpack.Contents.Inventory = v.gameObject,
                    none: () =>
                    {
                        Debug.Log("Backpack/Contents/Inventoryが見つかりませんでした");
                    }
                );

            model
                .Backpack.Contents.Self.gameObject.transform.Find("StatusPanel")
                .Match(
                    some: v => model.Backpack.Contents.StatusPanel = v.gameObject,
                    none: () =>
                    {
                        Debug.Log("Backpack/Contents/StatusPanelが見つかりませんでした");
                    }
                );
            GameObject
                .Find("Pointer")
                .Match(
                    some: v => model.Pointer = v,
                    none: () =>
                    {
                        Debug.Log("Pointerが見つかりませんでした");
                    }
                );

            GameObject
                .Find("Player")
                .Match(
                    some: v => model.Player = v,
                    none: () =>
                    {
                        Debug.Log("Playerが見つかりませんでした");
                    }
                );
        }

        void Start()
        {
            CheckIItemMetadata();
            LoadResources();
            DestroyRigidbodyOfCanvasItem();
        }

        private void DestroyRigidbodyOfCanvasItem()
        {
            foreach (
                var rb in model.Backpack.Contents.Inventory.GetComponentsInChildren<Rigidbody2D>()
            )
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
