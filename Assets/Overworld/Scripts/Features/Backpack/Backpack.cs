using Cinemachine;
using Overworld.Core;
using Overworld.Features.Pointer;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Backpack
{
    class Backpack : MonoBehaviour
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [SerializeField]
        private CinemachineVirtualCamera virtualCamera = default!;

        public bool open { get; private set; } = false;
        private CinemachineFramingTransposer? orbitalTransposer;

        void Start()
        {
            orbitalTransposer = virtualCamera
                .Except("BackpackにVirtualCameraがアタッチされていません")
                .GetCinemachineComponent<CinemachineFramingTransposer>();

            orbitalTransposer.m_ScreenX = 0.5f;

            CloseBackpack();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                var playerPointer = overworldModel.Pointer.GetComponent<PlayerPointer>();

                playerPointer.holdingItem.Match(
                    some: _ =>
                    {
                        if (playerPointer.locationStatus.value == LocationStatus.Location.Bag)
                        {
                            if (open)
                            {
                                return;
                            }
                        }

                        ToggleOpenBackpack();
                    },
                    none: () =>
                    {
                        ToggleOpenBackpack();
                    }
                );
            }
        }

        public void ToggleOpenBackpack()
        {
            open = !open;

            overworldModel.Backpack.Contents.Self.SetActive(open);
            orbitalTransposer.Match(v => v.m_ScreenY = open ? 0.4f : 0.5f);
        }

        public void OpenBackpack()
        {
            open = true;
            overworldModel.Backpack.Contents.Self.SetActive(true);
            orbitalTransposer.Match(v => v.m_ScreenY = 0.4f);
        }

        public void CloseBackpack()
        {
            open = false;
            overworldModel.Backpack.Contents.Self.SetActive(false);
            orbitalTransposer.Match(v => v.m_ScreenY = 0.5f);
        }
    }
}
