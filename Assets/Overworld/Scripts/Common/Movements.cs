using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Movements
{
    Vector2 currentVelocity = new Vector2();
    public void addForce(Rigidbody2D rb, Vector2 vec)
    {
        rb.AddForce(vec, ForceMode2D.Impulse);
    }

    public void changeVelocity(Rigidbody2D rb, Vector2 vec, float smoothTime)
    {
        rb.velocity = Vector2.SmoothDamp(rb.velocity, vec, ref currentVelocity, smoothTime); ;
    }

    public void toPosition(GameObject gameobject, Vector2 targetPosition, float speed)
    {
       gameobject.transform.position = Vector3.MoveTowards(gameobject.transform.position, targetPosition, Time.deltaTime * speed);
    }
}
