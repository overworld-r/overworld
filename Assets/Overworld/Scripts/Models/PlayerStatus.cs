using System;
using System.Collections;
using System.Collections.Generic;
using Overworld.Core;
using Overworld.Features.Resource.Models;
using UnityEngine;

namespace Overworld.Models
{
    public class ResourcesAmount
    {
        public ResourceType type { get; set; }
        public int amount { get; set; }

        public ResourcesAmount(ResourceType type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }
    }

    public class PlayerStatus
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();
        public List<ResourcesAmount> resourcesAmount = new List<ResourcesAmount>();

        public void ChangeGravity(float duration, float amount)
        {
            var rb = overworldModel.Player.gameObject.GetComponent<Rigidbody2D>();
            var beforeGravirty = rb.gravityScale;
            EffectivePeriod(
                duration,
                start: () =>
                {
                    rb.gravityScale = beforeGravirty * amount;
                },
                end: () =>
                {
                    rb.gravityScale = beforeGravirty;
                }
            );
        }

        private IEnumerator EffectivePeriod(float duration, Action start, Action end)
        {
            start();
            yield return new WaitForSeconds(duration);
            end();
        }
    }
}