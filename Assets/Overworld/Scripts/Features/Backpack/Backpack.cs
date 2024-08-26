using Cinemachine;
using Overworld.Core;
using Overworld.Models;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Backpack
{
    class Backpack : MonoBehaviour
    {
        public bool open = false;

        GameObject? canvas;

        [SerializeField]
        public CinemachineVirtualCamera virtualCamera = default!;

        CinemachineFramingTransposer? orbitalTransposer;

        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        void Start()
        {
            var canvasName = overworldModel.CanvasObjectName;
            canvas = overworldModel
                .Backpack.transform.Find(canvasName)
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
