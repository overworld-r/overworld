using UnityEngine;

namespace Overworld.Models
{
    public interface IClickable
    {
        void OnClick(GameObject itemPrefab);
    }
}
