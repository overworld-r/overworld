using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OneWay : MonoBehaviour
{
    public BoxCollider2D? objectCollider;

    void Start()
    {
        objectCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (objectCollider == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            objectCollider.enabled = false;
        }
        else
        {
            objectCollider.enabled = true;
        }
    }
}
