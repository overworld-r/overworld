using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;


public class ItemBuilder : MonoBehaviour
{
    private GameObject clickedGameObject;
    public GameObject grippedGameObject;
    
    void Start()
    {
    }

    void Update()
    {
        // Processing when clicked
        // Since we want the coordinates of the mouse, we are not using EventSystem
        if (Input.GetMouseButtonDown(0))
        {
            clickedGameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitSprite = Physics2D.Raycast(ray.origin, ray.direction);
            if (hitSprite)
            {
                clickedGameObject = hitSprite.transform.gameObject;
                ItemExistanceStatus clickedGameObjectItemExistanceStatus = clickedGameObject.GetComponent<ItemExistanceStatus>();
                if (clickedGameObject.CompareTag("Item"))
                {
                    grippedGameObject = clickedGameObjectItemExistanceStatus.OnClicked();
                }
            }
        }
        
        // While holding an item, follow the pointer coordinates
        if(!grippedGameObject) return;
        ItemExistanceStatus grippedObjectItemExistanceStatus = grippedGameObject.GetComponent<ItemExistanceStatus>();
        if(grippedObjectItemExistanceStatus.existanceStatus == ItemExistanceStatus.ExistanceStatus.Ghost)
        {
            Vector3 screenPosition = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(
        Mathf.Round(screenPosition.x / 100.0f) * 100.0f, Mathf.Round(screenPosition.y / 100.0f) * 100.0f, 10.0f));
            grippedGameObject.transform.position = worldPos;
            // Processing of rotation of the item being held
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