using Cinemachine;
using Overworld.Core;
using Overworld.Models;
using UnityEngine;

namespace Overworld.Backpack
{
    class Backpack : MonoBehaviour
    {
        public bool open = false;

        GameObject? canvas;
        public CinemachineVirtualCamera? virtualCamera;
        CinemachineFramingTransposer? orbitalTransposer;

        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        public void Start()
        {
            var canvasName = overworldModel.CanvasObjectName;
            canvas = overworldModel.Backpack?.transform.Find(canvasName).gameObject;
            orbitalTransposer =
                virtualCamera?.GetCinemachineComponent<CinemachineFramingTransposer>();

            if (orbitalTransposer != null)
                orbitalTransposer.m_ScreenX = 0.5f;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!canvas || !virtualCamera)
                    return;

                open = !open;

                canvas?.gameObject.SetActive(open);

                if (orbitalTransposer != null)
                    orbitalTransposer.m_ScreenY = open ? 0.4f : 0.5f;
            }
        }
    }
}
