using Overworld.Core;
using Overworld.Model;
using UnityEngine;

namespace Overworld.Item.Functions
{
    public class ItemLocationManager : MonoBehaviour
    {
        [SerializeField]
        private Transform? bagParent;

        [SerializeField]
        private Backpack.Backpack? backpackComponent;

        private float locationLine = Screen.height - Screen.height / 2;

        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        void Reset()
        {
            if (overworldModel.Backpack == null)
                return;

            var canvasName = overworldModel.CanvasObjectName;
            bagParent = overworldModel.Backpack?.transform.Find(canvasName).transform;
            backpackComponent = overworldModel.Backpack?.GetComponent<Backpack.Backpack>();
        }

        public void UpdateItemLocation(ItemBase item)
        {
            if (backpackComponent?.open == false)
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
