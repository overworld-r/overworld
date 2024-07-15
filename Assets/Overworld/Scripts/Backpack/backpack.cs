using Cinemachine;
using UnityEngine;

namespace Backpack
{
    class Backpack : MonoBehaviour
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
                open = !open;
                if (!canvas || !virtualCamera)
                    return;

                canvas.SetActive(open);
                if (open)
                {
                    orbitalTransposer.m_ScreenY = 0.2f;
                }
                else
                {
                    orbitalTransposer.m_ScreenY = 0.5f;
                }
            }
        }
    }
}
