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
        private GameObject? canvas;
        private CinemachineFramingTransposer? orbitalTransposer;

        void Start()
        {
            var canvasName = overworldModel.CanvasObjectName;
            canvas = overworldModel
                .Backpack.Except("Bacpack is not assigned")
                .transform.Find(canvasName)
                .Except("Canvas not found")
                .gameObject;
            orbitalTransposer =
                virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

            orbitalTransposer.m_ScreenX = 0.5f;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!canvas || !virtualCamera)
                    return;

                open = !open;

                canvas.Unwrap().gameObject.SetActive(open);
                orbitalTransposer.Unwrap().m_ScreenY = open ? 0.4f : 0.5f;
            }
        }
    }
}
