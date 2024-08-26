using Overworld.Core;
using Overworld.Models;
using Overworld.Types;
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
            PlayerPointer _playerPointer = playerPointer.Unwrap();

            if (backpackComponent.Unwrap().open == false)
            {
                _playerPointer.locationStatus = OverworldModel.LocationStatus.World;
                return;
            }

            if (_playerPointer.locationStatus == OverworldModel.LocationStatus.Bag)
            {
                if (Input.mousePosition.y >= locationLine)
                {
                    _playerPointer.locationStatus = OverworldModel.LocationStatus.World;
                }
            }
            else
            {
                if (Input.mousePosition.y < locationLine)
                {
                    _playerPointer.locationStatus = OverworldModel.LocationStatus.Bag;
                }
            }
        }
    }
}
