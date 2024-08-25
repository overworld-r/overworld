using Overworld.Core;
using Overworld.Features.Item.Models;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(PointerMover))]
    [RequireComponent(typeof(PointerLocationManager))]
    [RequireComponent(typeof(PointerHoldingLocationManager))]
    [RequireComponent(typeof(PointerClickHandler))]
    [RequireComponent(typeof(PointerRotator))]
    public class PlayerPointer : MonoBehaviour
    {
        [SerializeField]
        private PointerClickHandler? itemClickHandler;

        [SerializeField]
        private PointerMover? itemMover;

        [SerializeField]
        private PointerRotator? itemRotator;

        [SerializeField]
        private PointerHoldingLocationManager? pointerHoldingLocationManager;

        [SerializeField]
        private PointerLocationManager? PointerLocationManager;

        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        public OverworldModel.LocationStatus locationStatus = OverworldModel.LocationStatus.World;

        public IOption<GameObject> holdingItem = new None<GameObject>();

        void Reset()
        {
            itemClickHandler = GetComponent<PointerClickHandler>();
            itemMover = GetComponent<PointerMover>();
            itemRotator = GetComponent<PointerRotator>();
            pointerHoldingLocationManager = GetComponent<PointerHoldingLocationManager>();
            PointerLocationManager = GetComponent<PointerLocationManager>();
        }

        private void Start()
        {
            if (itemClickHandler == null)
                return;

            itemClickHandler.OnItemClicked += HandleItemClick;
        }

        private void Update()
        {
            PointerLocationManager?.UpdatePointerLocation();
            if (!holdingItem.IsEmpty)
            {
                var itemBase = holdingItem.Value.GetComponent<ItemBase>();
                pointerHoldingLocationManager?.UpdateItemLocation(itemBase);
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
                clickable.OnClick(itemPrefab);
            }
        }
    }
}
