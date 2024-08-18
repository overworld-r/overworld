using Overworld.Core;
using Overworld.Model;
using Overworld.Types;
using UnityEngine;

namespace Overworld.Item.Functions
{
    public class ItemClickHandler : MonoBehaviour
    {
        public event System.Action<GameObject>? OnItemClicked;
        private IOption<GameObject> heldItem = new None<GameObject>();
        OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                heldItem.Match(
                    some: item =>
                    {
                        OnItemClicked?.Invoke(item);
                    },
                    none: () =>
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        if (
                            overworldModel.cursorLocationStatus == OverworldModel.LocationStatus.Bag
                            && overworldModel.UICamera != null
                        )
                        {
                            ray = overworldModel.UICamera.ScreenPointToRay(Input.mousePosition);
                        }

                        RaycastHit2D hitSprite = Physics2D.Raycast(ray.origin, ray.direction);

                        if (hitSprite != false && hitSprite.transform.gameObject.CompareTag("Item"))
                        {
                            OnItemClicked?.Invoke(hitSprite.transform.gameObject);
                        }
                    }
                );
            }
        }

        public void SetHeldItem(IOption<GameObject> item)
        {
            heldItem = item;
        }
    }
}
