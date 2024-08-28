using Overworld.Core;
using Overworld.Models;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    using Models;

    [RequireComponent(typeof(BoxCollider2D))]
    class Breakable : MonoBehaviour, IClickable
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        private Renderer? breakableRenderer;
        private PlayerPointer playerPointer = default!;

        private float breakProgress = 0.0f;
        private float breakDuration = 0.0f;
        private bool shouldStopBreak = true;
        private float startBreakingTime = 0.0f;

        void Start()
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
                StopBreake();
            }

            if (shouldStopBreak)
            {
                startBreakingTime = Time.time;
                breakableRenderer?.material.SetFloat("_CrackProgress", 0);
                return;
            }

            breakProgress = (Time.time - startBreakingTime) / breakDuration;
            breakableRenderer?.material.SetFloat("_CrackProgress", breakProgress);

            if (breakProgress >= 1f)
            {
                Destroy(this.gameObject);
                if (TryGetComponent<Models.IBreakable>(out var breakable))
                {
                    breakable.OnBreak();
                }
            }
        }

        public void StartBreak(float duration)
        {
            shouldStopBreak = false;
            breakDuration = duration;
        }

        public void StopBreake()
        {
            shouldStopBreak = true;
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

            StartBreak(1.2f);
        }
    }
}