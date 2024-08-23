using Overworld.Core;
using Overworld.Model;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Item.Functions
{
    public class ItemMover : MonoBehaviour
    {
        private IOption<GameObject> heldItem = new None<GameObject>();
        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        void Update()
        {
            heldItem.Match(
                none: () =>
                {
                    return;
                },
                some: item =>
                {
                    Vector3 mousePosition = Input.mousePosition;
                    mousePosition.z = 10f;

                    item.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

                    if (overworldModel.cursorLocationStatus == OverworldModel.LocationStatus.World)
                    {
                        item.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
                    }
                    else
                    {
                        if (overworldModel.UICamera != null)
                            item.transform.position = overworldModel.UICamera.ScreenToWorldPoint(
                                mousePosition
                            );
                    }
                }
            );
        }

        public void SetHeldItem(IOption<GameObject> item)
        {
            heldItem = item;
        }
    }
}