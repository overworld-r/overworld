using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayTrigger : MonoBehaviour
{
    void Start()
    {
        OneWay = GetComponentInParent<OneWay>();
    }

    private OneWay OneWay = default!;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            OneWay.Exit();
        }
    }
}
