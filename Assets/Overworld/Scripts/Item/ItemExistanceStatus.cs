using UnityEngine;

public class ItemStatusHandler : MonoBehaviour
{
    public enum ExistanceStatus
    {
        None,
        Ghost,
        Physical,
    }

    public ExistanceStatus existanceStatus;

    public ItemStatusHandler()
    {
        existanceStatus = ExistanceStatus.Ghost;
    }

    public void OnClicked()
    {
        switch (existanceStatus)
        {
            case ExistanceStatus.Physical:
                OnClickedWhenItemStatusIsPhysical();
                break;
            case ExistanceStatus.Ghost:
                OnClickedWhenItemStatusIsGhost();
                break;
            case ExistanceStatus.None:
                OnClickedWhenItemStatusIsNone();
                break;
            default:
                break;
        }
    }

    public void OnClickedWhenItemStatusIsPhysical()
    {
        Destroy(this.gameObject);
        var newGameObject = Instantiate(this.gameObject);
        newGameObject.GetComponent<ItemStatusHandler>().existanceStatus = ExistanceStatus.Ghost;
        newGameObject.AddComponent<Rigidbody2D>();
        newGameObject.GetComponent<Collider2D>().isTrigger = true;
    }

    public void OnClickedWhenItemStatusIsGhost()
    {
        Destroy(this.gameObject);
        var newGameObject = Instantiate(this.gameObject);
        newGameObject.GetComponent<ItemStatusHandler>().existanceStatus = ExistanceStatus.Physical;
        newGameObject.GetComponent<Collider2D>().isTrigger = false;
    }

    public void OnClickedWhenItemStatusIsNone()
    {
        Destroy(this.gameObject);
    }
}
