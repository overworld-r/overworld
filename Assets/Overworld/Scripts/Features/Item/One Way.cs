using Overworld.Types;
using UnityEngine;

public class DisableColliderOnButtonPress : MonoBehaviour
{
    public BoxCollider2D? objectCollider;

    void Start()
    {
        if (objectCollider == null)
        {
            objectCollider = GetComponent<BoxCollider2D>();
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            objectCollider.Unwrap().enabled = false;
        }
        else
        {
            objectCollider.Unwrap().enabled = true;
        }
    }
}
