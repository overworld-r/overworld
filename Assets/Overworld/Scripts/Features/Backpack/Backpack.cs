using Cinemachine;
using Overworld.Core;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Backpack
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
            orbitalTransposer =
                virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

            orbitalTransposer.m_ScreenX = 0.5f;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                open = !open;

                overworldModel.Canvas.SetActive(open);
                orbitalTransposer.Match(v => v.m_ScreenY = open ? 0.4f : 0.5f);
            }
        }
    }
}
