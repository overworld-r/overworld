using Cinemachine;
using UnityEngine;

namespace Overworld.Backpack
{
    public class Backpack : MonoBehaviour
    {
        public bool open = false;

        GameObject canvas;
        public CinemachineVirtualCamera virtualCamera;
        CinemachineFramingTransposer orbitalTransposer;

        public void Start()
        {
            canvas = transform.Find("Canvas").gameObject;
            orbitalTransposer =
                virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            orbitalTransposer.m_ScreenX = 0.5f;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {

                if (!canvas || !virtualCamera)
                    return;

                open = !open;

                canvas.SetActive(open);
                orbitalTransposer.m_ScreenY = open ? 0.4f : 0.5f;
            }
        }
    }
}