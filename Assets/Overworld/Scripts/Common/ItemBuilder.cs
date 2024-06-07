using UnityEditor;
using UnityEngine;


public class ItemBuilder : MonoBehaviour
{
    private GameObject clickedGameObject;
    public GameObject grippedObject;
    
    void Start()
    {
    }

    void Update()
    {
        //マウスの座標がほしいので、EventSystemは使ってないよ
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click!!!!");
            clickedGameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitSprite = Physics2D.Raycast(ray.origin, ray.direction);
            if (hitSprite)
            {
                clickedGameObject = hitSprite.transform.gameObject;
                ItemExistanceStatus clickedGameObjectItemExistanceStatus = clickedGameObject.GetComponent<ItemExistanceStatus>();
                if (clickedGameObject.CompareTag("Item"))
                {
                    grippedObject = clickedGameObjectItemExistanceStatus.OnClicked();
                }
            }
        } 
        if(!grippedObject) return;
        ItemExistanceStatus grippedObjectItemExistanceStatus = grippedObject.GetComponent<ItemExistanceStatus>();
        if(grippedObjectItemExistanceStatus.existanceStatus == ItemExistanceStatus.ExistanceStatus.Ghost)
        {
            Vector3 screenPosition = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(
        Mathf.Round(screenPosition.x / 100.0f) * 100.0f, Mathf.Round(screenPosition.y / 100.0f) * 100.0f, 10.0f));
            grippedObject.transform.position = worldPos;
        }
    }
}