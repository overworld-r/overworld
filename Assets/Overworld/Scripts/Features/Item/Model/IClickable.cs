using Overworld.Types;
using UnityEngine;

namespace Overworld.Item.Model
{
    public interface IClickable
    {
        IOption<GameObject> OnClick(GameObject itemPrefab);
    }
}
