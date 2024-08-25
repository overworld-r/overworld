using Overworld.Core;
using Overworld.Models;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(PlayerPointer))]
    public class PointerLocationManager : MonoBehaviour
    {
        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [SerializeField]
        private Backpack.Backpack? backpackComponent;

        [SerializeField]
        PlayerPointer? playerPointer;

        private float locationLine = Screen.height - Screen.height / 2;

        void Reset()
        {
            if (overworldModel.Backpack == null)
                return;

            backpackComponent = overworldModel.Backpack?.GetComponent<Backpack.Backpack>();
            playerPointer = GetComponent<PlayerPointer>();
        }

        public void UpdatePointerLocation()
        {
            if (playerPointer == null)
            {
                throw new System.Exception("PlayerPointer is null");
            }

            if (backpackComponent?.open == false)
            {
                playerPointer.locationStatus = OverworldModel.LocationStatus.World;
                return;
            }

            if (playerPointer.locationStatus == OverworldModel.LocationStatus.Bag)
            {
                if (Input.mousePosition.y >= locationLine)
                {
                    playerPointer.locationStatus = OverworldModel.LocationStatus.World;
                }
            }
            else
            {
                if (Input.mousePosition.y < locationLine)
                {
                    playerPointer.locationStatus = OverworldModel.LocationStatus.Bag;
                }
            }
        }
    }
}
