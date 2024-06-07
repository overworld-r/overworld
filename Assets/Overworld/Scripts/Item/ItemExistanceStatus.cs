using System;
using System.Runtime.Serialization;
using UnityEditor.UI;
using UnityEngine;

public class ItemExistanceStatus : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    
    public enum ExistanceStatus
    {
        None,
        Ghost,
        Physical,
    }

    public bool isStack
    {
        get;
        private set;
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
        var newGameObject = Instantiate(itemPrefab);
        newGameObject.GetComponent<DebugItem>().enabled = true;
        ItemExistanceStatus newGameObjectItemExistanceStatus = newGameObject.GetComponent<ItemExistanceStatus>();
        newGameObjectItemExistanceStatus.existanceStatus = ExistanceStatus.Ghost;
        newGameObject.AddComponent<Rigidbody2D>();
        newGameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        newGameObject.GetComponent<Collider2D>().enabled = true;
        newGameObject.GetComponent<Collider2D>().isTrigger = true;
        return newGameObject;
    }

    public GameObject OnClickedWhenItemStatusIsGhost()
    {
        if (isStack == false)
        {
            Destroy(this.gameObject);
            var newGameObject = Instantiate(itemPrefab);
            newGameObject.GetComponent<DebugItem>().enabled = true;
            ItemExistanceStatus newGameObjectItemExistanceStatus = newGameObject.GetComponent<ItemExistanceStatus>();
            newGameObjectItemExistanceStatus.existanceStatus = ExistanceStatus.Physical;
            Destroy(newGameObject.GetComponent<Rigidbody2D>());
            newGameObject.GetComponent<Collider2D>().enabled = true;
            newGameObject.GetComponent<Collider2D>().isTrigger = false;
            return newGameObject;
        }
        else
        {
            return this.gameObject;
        }
    }

    public GameObject OnClickedWhenItemStatusIsNone()
    {
        Destroy(this.gameObject);
        return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isStack = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        isStack = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isStack = false;
    }
}
