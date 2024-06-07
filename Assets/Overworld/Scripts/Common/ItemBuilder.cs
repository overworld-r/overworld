using UnityEngine;


public class ItemBuilder : MonoBehaviour
{
    private GameObject clickedGameObject;
    private GameObject grippedObject;
    
    void Start()
    {
    }

    void Update()
    {
        //マウスの座標がほしいので、EventSystemは使ってないよ
        if (Input.GetMouseButtonDown(0))
        {
            clickedGameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitSprite = Physics2D.Raycast(ray.origin, ray.direction);
            if (hitSprite)
            {
                clickedGameObject = hitSprite.transform.gameObject;
                Item itemComponent = clickedGameObject.GetComponent<Item>();
                if (clickedGameObject.CompareTag("Item"))
                {
                    itemComponent.OnClicked();
                }
            }
        }

        if(!grippedObject) return;
        if (grippedObject.GetComponent<Item>().existanceStatus == Item.ExistanceStatus.Ghost)
        {
            Vector3 screenPosition = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(
        Mathf.Round(screenPosition.x / 100.0f) * 100.0f, Mathf.Round(screenPosition.y / 100.0f) * 100.0f));
            grippedObject.transform.position = worldPos;
        }
    }
}