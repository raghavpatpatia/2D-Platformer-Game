using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int startingPoint;
    private int i;

    private void Start()
    {
        transform.position = patrolPoints[startingPoint].position;
    }

    private void Update()
    {
        BridgeMovement();
    }

    private void BridgeMovement()
    {
        if (Vector2.Distance(transform.position, patrolPoints[i].position) < 0.2f)
        {
            i++;
            if (i == patrolPoints.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[i].position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
