using Overworld.Core;
using Overworld.Features.Item.Models;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    public class PointerHoldingLocationManager : MonoBehaviour
    {
        [SerializeField]
        private Transform? bagParent;

        [SerializeField]
        private Backpack.Backpack? backpackComponent;

        private float locationLine = Screen.height - Screen.height / 2;

        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        void Reset()
        {
            var canvasName = overworldModel.CanvasObjectName;
            bagParent = overworldModel.Backpack?.transform.Find(canvasName).transform;
            backpackComponent = overworldModel.Backpack?.GetComponent<Backpack.Backpack>();
        }

        public void UpdateItemLocation(ItemBase item)
        {
            if (backpackComponent.Unwrap().open == false)
            {
                return;
            }

            if (item.locationStatus == OverworldModel.LocationStatus.Bag)
            {
                if (Input.mousePosition.y >= locationLine)
                {
                    item.ChangeLocationStatus(
                        item.gameObject,
                        this.transform,
                        OverworldModel.LocationStatus.World
                    );
                }
            }
            else
            {
                if (Input.mousePosition.y < locationLine && bagParent is Transform _bagParent)
                {
                    item.ChangeLocationStatus(
                        item.gameObject,
                        _bagParent,
                        OverworldModel.LocationStatus.Bag
                    );
                }
            }
        }
    }
}