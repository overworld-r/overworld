using Overworld.Core;
using Overworld.Features.Pointer.Models;
using Overworld.Models;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    [RequireComponent(typeof(BoxCollider2D))]
    class Breakable : MonoBehaviour, IClickable
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [SerializeField]
        private Renderer breakableRenderer = default!;

        private PlayerPointer playerPointer = default!;

        private bool isMouseDown = false;
        private float mouseDownTime = 0f;

        void Reset()
        {
            breakableRenderer = GetComponent<Renderer>();
        }

        void Awake()
        {
            playerPointer = overworldModel.Pointer.GetComponent<PlayerPointer>();
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                isMouseDown = false;
            }

            if (!isMouseDown)
            {
                mouseDownTime = Time.time;
                breakableRenderer.material.SetFloat("_CrackProgress", 0);
                return;
            }

            float progress = (Time.time - mouseDownTime) / 1.5f;
            breakableRenderer.material.SetFloat("_CrackProgress", progress);

            if (progress >= 1f)
            {
                Destroy(this.gameObject);
                if (TryGetComponent<Models.IBreakable>(out var breakable))
                {
                    breakable.OnBreak();
                }
            }
        }

        void IClickable.OnClick(GameObject itemPrefab)
        {
            if (
                playerPointer.locationStatus.value != LocationStatus.Location.World
                || !playerPointer.holdingItem.IsEmpty
            )
            {
                return;
            }

            isMouseDown = true;
        }
    }
}
