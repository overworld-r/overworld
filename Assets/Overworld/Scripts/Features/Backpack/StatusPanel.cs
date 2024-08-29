using System.Collections.Generic;
using System.Linq;
using Overworld.Core;
using Overworld.Features.Resource.Models;
using Overworld.Models;
using Overworld.Types;
using TMPro;
using UnityEngine;

namespace Overworld.Features.Backpack
{
    class ListItem
    {
        public ResourceType resourceType;
        public GameObject gameObject;

        public ListItem(ResourceType resourceType, GameObject gameObject)
        {
            this.resourceType = resourceType;
            this.gameObject = gameObject;
        }
    }

    class StatusPanel : MonoBehaviour
    {
        private OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        [SerializeField]
        private GameObject listItemPrefab = default!;

        [SerializeField]
        private float padding = default!;

        private List<ListItem> listItems = new List<ListItem>();

        void Start() { }

        void Update()
        {
            UpdateList();
        }

        public void UpdateList()
        {
            foreach (var resource in overworldModel.PlayerStatus.resourcesAmount)
            {
                listItems
                    .Find(v => v.resourceType == resource.type)
                    .Match(
                        some: v =>
                        {
                            v.gameObject.GetComponentInChildren<TextMeshProUGUI>().text =
                                resource.amount.ToString();
                        },
                        none: () =>
                        {
                            if (resource.amount < 0)
                                return;

                            Vector3 listPosition =
                                listItems.Count == 0
                                    ? overworldModel
                                        .Backpack
                                        .Contents
                                        .StatusPanel
                                        .transform
                                        .position
                                    : new Vector3(
                                        listItems.LastOrDefault().gameObject.transform.position.x,
                                        listItems.LastOrDefault().gameObject.transform.position.y
                                            - padding,
                                        listItems.LastOrDefault().gameObject.transform.position.z
                                    );

                            GameObject newListItem = Instantiate(
                                listItemPrefab,
                                listPosition,
                                Quaternion.identity,
                                overworldModel.Backpack.Contents.StatusPanel.transform
                            );

                            overworldModel
                                .ResourcePrefabs.Find(v =>
                                    v.GetComponent<IResourceMetadata>().type == resource.type
                                )
                                .Match(resourcePrefab =>
                                {
                                    var existResource = newListItem.transform.Find("Resource");
                                    var newResource = Instantiate(
                                        resourcePrefab,
                                        existResource.position,
                                        Quaternion.identity,
                                        newListItem.transform
                                    );
                                    newResource.transform.localScale *= 20;
                                    newResource.OptGetComponent<Rigidbody2D>(rb =>
                                    {
                                        Destroy(rb);
                                    });

                                    newResource.OptGetComponent<Resource.Resource>(
                                        resourceComponent =>
                                        {
                                            Destroy(resourceComponent);
                                        }
                                    );
                                    Destroy(existResource.gameObject);
                                });

                            listItems.Add(new ListItem(resource.type, newListItem));
                        }
                    );
            }
        }
    }
}
