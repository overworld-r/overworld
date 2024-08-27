using UnityEngine;

namespace Overworld.Features.Pointer.Models
{
    public interface IClickable
    {
        void OnClick(GameObject itemPrefab) { }
    }
}