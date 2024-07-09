using UnityEngine;
using Overworld.Model;

public class MovingPlatform : ItemPlaceable
{
    public float moveSpeed = 2f;
    public float moveDistance = 3f;

    private Vector3 startPosition;
    private bool movingUp = true;

    public override string itemName =>"moveBox";

    public override string description => "Moving Box";
    public override int price => 100000;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 targetPosition;

        if (movingUp)
        {
            targetPosition = startPosition + Vector3.up * moveDistance;
        }
        else
        {
            targetPosition = startPosition - Vector3.up * moveDistance;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            movingUp = !movingUp;
        }
    }
}
