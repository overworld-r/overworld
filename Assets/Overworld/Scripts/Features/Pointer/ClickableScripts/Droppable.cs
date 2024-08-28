using System.Collections.Generic;
using Overworld.Core;
using Overworld.Features.Resource.Models;
using Overworld.Mechanics.Types;
using Overworld.Models;
using UnityEngine;

namespace Overworld.Features.Pointer
{
    using Models;

    [System.Serializable]
    public class DropAmount
    {
        [SerializeField]
        public ResourceType type = ResourceType.Stone;

        [SerializeField]
        public int value = 0;
    }

    public class Droppable : MonoBehaviour, IBreakable
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [SerializeField]
        List<DropAmount> droppAmountList = new List<DropAmount>();

        void Update() { }

        void Models.IBreakable.OnBreak()
        {
            GenerateResource();
        }

        void GenerateResource()
        {
            foreach (var amount in droppAmountList)
            {
                for (int i = 0; i < amount.value; i++)
                {
                    Vector3 random = new Vector3(
                        Random.Range(-0.5f, 0.5f),
                        Random.Range(-0.5f, 0.5f),
                        0.0f
                    );

                    var prefab = overworldModel.ResourcePrefabs.Find(v =>
                        v.GetComponent<IResourceMetadata>().type == amount.type
                    );
                    var newResource = Instantiate(prefab);

                    newResource.transform.position = this.transform.position + random;
                    newResource.GetComponent<Rigidbody2D>().Push(random);
                }
            }
        }
    }
}
