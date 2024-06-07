using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public abstract string name { get; }
    public abstract string description { get; }
    public abstract int price { get; }

    public enum ExistanceStatus
    {
        None,
        Ghost,
        Phisical,
    }

    public ExistanceStatus existanceStatus;
    private Dictionary<ExistanceStatus, ItemClickHandler> clickHandler;

    public Item()
    {
        existanceStatus = ExistanceStatus.Ghost;
        clickHandler = new Dictionary<ExistanceStatus, ItemClickHandler>
        {
            { ExistanceStatus.None, new ItemNoneClickHandler() },
            { ExistanceStatus.Ghost, new ItemGhostClickHandler() },
            { ExistanceStatus.Phisical, new ItemPhisicsClickHandler() },
        };
    }

    public void OnClicked()
    {
        clickHandler[existanceStatus].OnClicked();
    }

    public virtual void OnBuilt()
    {
        OnEnable();
    }

    public virtual void OnBroken()
    {
        OnDisable();
    }

    public virtual void OnEnable() { }

    public virtual void OnDisable() { }
}

public interface ItemClickHandler
{
    void OnClicked();
}

public class ItemGhostClickHandler : MonoBehaviour, ItemClickHandler
{
    public void OnClicked()
    {
        Destroy(this.gameObject);
        var newGameObject = Instantiate(this.gameObject);
        newGameObject.GetComponent<Item>().existanceStatus = Item.ExistanceStatus.Phisical;
    }
}

public class ItemPhisicsClickHandler : MonoBehaviour, ItemClickHandler
{
    public void OnClicked()
    {
        Destroy(this.gameObject);
        var newGameObject = Instantiate(this.gameObject);
        newGameObject.GetComponent<Item>().existanceStatus = Item.ExistanceStatus.Ghost;
    }
}

public class ItemNoneClickHandler : MonoBehaviour, ItemClickHandler
{
    public void OnClicked()
    {
        Destroy(this.gameObject);
    }
}
