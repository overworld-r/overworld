using Overworld.Core;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    using Models;

    [RequireComponent(typeof(PointerMover))]
    [RequireComponent(typeof(PointerLocationManager))]
    [RequireComponent(typeof(PointerClickHandler))]
    [RequireComponent(typeof(PointerRotator))]
    public class PlayerPointer : MonoBehaviour
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        private PointerClickHandler pointerClickHandler = default!;
        private PointerMover pointerMover = default!;
        private PointerRotator pointerRotator = default!;
        private PointerLocationManager pointerLocationManager = default!;

        public LocationStatus locationStatus = new LocationStatus(LocationStatus.Location.World);
        public IOption<GameObject> holdingItem = new None<GameObject>();

        private void Start()
        {
            pointerClickHandler = GetComponent<PointerClickHandler>();
            pointerClickHandler.OnItemClicked += HandleItemClick;
            pointerClickHandler = GetComponent<PointerClickHandler>();
            pointerMover = GetComponent<PointerMover>();
            pointerRotator = GetComponent<PointerRotator>();
            pointerLocationManager = GetComponent<PointerLocationManager>();
        }

        private void Update()
        {
            pointerLocationManager.UpdatePointerLocation();
        }

        private void HandleItemClick(GameObject clickedItem)
        {
            var clickableComponents = clickedItem.GetComponents<IClickable>();
            foreach (var clickable in clickableComponents)
            {
                Resources
                    .Load<GameObject>($"ItemPrefabs/{clickedItem.name}")
                    .Match(
                        some: (p) =>
                        {
                            clickable.OnClick(p);
                        },
                        none: () =>
                        {
                            Debug.LogError($"プレファブが見つかりません: {clickedItem.name}");
                        }
                    );
            }
        }
    }
}
