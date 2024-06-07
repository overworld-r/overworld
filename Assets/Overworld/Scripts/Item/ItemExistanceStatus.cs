using UnityEngine;

public class ItemExistanceStatus : MonoBehaviour
{
    public enum ExistanceStatus
    {
        None,
        Ghost,
        Physical,
    }

    public ExistanceStatus existanceStatus;

    public ItemExistanceStatus()
    {
        existanceStatus = ExistanceStatus.Ghost;
    }

    public GameObject OnClicked()
    {
        switch (existanceStatus)
        {
            case ExistanceStatus.Physical:
                return OnClickedWhenItemStatusIsPhysical();
            case ExistanceStatus.Ghost:
                return OnClickedWhenItemStatusIsGhost();
            case ExistanceStatus.None:
                return OnClickedWhenItemStatusIsNone();
        }

        return null;
    }

    public GameObject OnClickedWhenItemStatusIsPhysical()
    {
        Destroy(this.gameObject);
        var newGameObject = Instantiate(this.gameObject);
        newGameObject.GetComponent<ItemExistanceStatus>().existanceStatus = ExistanceStatus.Ghost;
        newGameObject.AddComponent<Rigidbody2D>();
        newGameObject.GetComponent<Collider2D>().isTrigger = true;
        return newGameObject;
    }

    public GameObject OnClickedWhenItemStatusIsGhost()
    {
        Destroy(this.gameObject);
        var newGameObject = Instantiate(this.gameObject);
        newGameObject.GetComponent<ItemExistanceStatus>().existanceStatus =
            ExistanceStatus.Physical;
        newGameObject.GetComponent<Collider2D>().isTrigger = false;
        return newGameObject;
    }

    public GameObject OnClickedWhenItemStatusIsNone()
    {
        Destroy(this.gameObject);
        return null;
    }
}
