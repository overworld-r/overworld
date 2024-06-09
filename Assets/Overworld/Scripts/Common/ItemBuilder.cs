using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemBuilder : MonoBehaviour
{
    private GameObject clickedGameObject;
    public GameObject grippedGameObject;

    void Start() { }

    void Update()
    {
        //クリックしたときの処理
        //マウスの座標がほしいので、EventSystemは使ってないよ
        if (Input.GetMouseButtonDown(0))
        {
            clickedGameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitSprite = Physics2D.Raycast(ray.origin, ray.direction);
            if (hitSprite)
            {
                clickedGameObject = hitSprite.transform.gameObject;
                ItemExistanceStatus clickedGameObjectItemExistanceStatus =
                    clickedGameObject.GetComponent<ItemExistanceStatus>();
                if (clickedGameObject.CompareTag("Item"))
                {
                    grippedGameObject = clickedGameObjectItemExistanceStatus.OnClicked();
                }
            }
        }

        //アイテムを持ってる間、座標をポインターに追従させる
        if (!grippedGameObject)
            return;
        ItemExistanceStatus grippedObjectItemExistanceStatus =
            grippedGameObject.GetComponent<ItemExistanceStatus>();
        if (
            grippedObjectItemExistanceStatus.existanceStatus
            == ItemExistanceStatus.ExistanceStatus.Ghost
        )
        {
            Vector3 screenPosition = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(screenPosition.x, screenPosition.y, 10.0f)
            );

            grippedGameObject.transform.position = new Vector3(
                Mathf.Round(worldPos.x),
                Mathf.Round(worldPos.y),
                worldPos.z
            );
            //持っているアイテムの回転の処理
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                grippedGameObject.transform.Rotate(0, 0, 90);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                grippedGameObject.transform.Rotate(0, 0, -90);
            }
        }
    }
}
