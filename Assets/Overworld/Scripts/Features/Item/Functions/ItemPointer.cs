using Overworld.Core;
using Overworld.Item.Model;
using Overworld.Model;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Item.Functions
{
    [RequireComponent(typeof(ItemMover))]
    [RequireComponent(typeof(ItemLocationManager))]
    [RequireComponent(typeof(CursorLocationManager))]
    [RequireComponent(typeof(ItemClickHandler))]
    [RequireComponent(typeof(ItemRotator))]
    public class ItemPointer : MonoBehaviour
    {
        [SerializeField]
        private ItemClickHandler? itemClickHandler;

        [SerializeField]
        private ItemMover? itemMover;

        [SerializeField]
        private ItemRotator? itemRotator;

        [SerializeField]
        private ItemLocationManager? itemLocationManager;

        [SerializeField]
        private CursorLocationManager? cursorLocationManager;

        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        private IOption<ItemBase> currentlyHeldItemComponent = new None<ItemBase>();

        void Reset()
        {
            itemClickHandler = GetComponent<ItemClickHandler>();
            itemMover = GetComponent<ItemMover>();
            itemRotator = GetComponent<ItemRotator>();
            itemLocationManager = GetComponent<ItemLocationManager>();
            cursorLocationManager = GetComponent<CursorLocationManager>();
        }

        private void Start()
        {
            if (itemClickHandler == null)
                return;

            itemClickHandler.OnItemClicked += HandleItemClick;
        }

        private void Update()
        {
            cursorLocationManager?.UpdateCursorLocation();
            if (!currentlyHeldItemComponent.IsEmpty)
            {
                itemLocationManager?.UpdateItemLocation(currentlyHeldItemComponent.Value);
            }
        }

        private void HandleItemClick(GameObject clickedItem)
        {
            var clickableComponents = clickedItem.GetComponents<IClickable>();
            foreach (var clickable in clickableComponents)
            {
                var itemPrefab = overworldModel.ItemPrefabs.Find(prefab =>
                    prefab.name == clickedItem.name
                );

                IOption<GameObject> result = clickable.OnClick(itemPrefab);

                currentlyHeldItemComponent = result.Match<IOption<ItemBase>>(
                    none: () => new None<ItemBase>(),
                    some: i => new Some<ItemBase>(i.GetComponent<ItemBase>())
                );

                itemClickHandler?.SetHeldItem(result);
                itemMover?.SetHeldItem(result);
                itemRotator?.SetHeldItem(result);

                if (result.IsEmpty)
                {
                    return;
                }
            }
        }
    }
}