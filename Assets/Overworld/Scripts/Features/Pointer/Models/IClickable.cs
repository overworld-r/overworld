using Overworld.Types;
using UnityEngine;

namespace Overworld.Features.Pointer.Models
{
    public interface IClickable
    {
        void OnClick(IOption<GameObject> itemPrefab) { }
    }
}