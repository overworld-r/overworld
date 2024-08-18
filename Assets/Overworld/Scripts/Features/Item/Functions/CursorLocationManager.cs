using Overworld.Core;
using Overworld.Model;
using UnityEngine;

namespace Overworld.Item.Functions
{
    public class CursorLocationManager : MonoBehaviour
    {
        private float locationLine = Screen.height - Screen.height / 2;

        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();
        private Backpack.Backpack? backpackComponent;

        void Start()
        {
            backpackComponent = overworldModel.Backpack?.GetComponent<Backpack.Backpack>();
        }

        public void UpdateCursorLocation()
        {
            if (backpackComponent?.open == false)
            {
                overworldModel.cursorLocationStatus = OverworldModel.LocationStatus.World;
                return;
            }

            if (overworldModel.cursorLocationStatus == OverworldModel.LocationStatus.Bag)
            {
                if (Input.mousePosition.y >= locationLine)
                {
                    overworldModel.cursorLocationStatus = OverworldModel.LocationStatus.World;
                }
            }
            else
            {
                if (Input.mousePosition.y < locationLine)
                {
                    overworldModel.cursorLocationStatus = OverworldModel.LocationStatus.Bag;
                }
            }
        }
    }
}
