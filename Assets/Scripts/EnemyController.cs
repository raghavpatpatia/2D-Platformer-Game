using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float speed;
    private Transform currentPosition;
    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        currentPosition = pointB.transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 scale = transform.localScale;
        if (currentPosition == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
            scale.x = Mathf.Abs(scale.x);
        }
        else if (currentPosition == pointA.transform)
        {
            rb.velocity = new Vector2(-speed, 0);
            scale.x = -1 * Mathf.Abs(scale.x);
        }

        if (Vector2.Distance(transform.position, currentPosition.position) < 1f && currentPosition == pointB.transform)
        {
            currentPosition = pointA.transform;          
        }

        if (Vector2.Distance(transform.position, currentPosition.position) < 1f && currentPosition == pointA.transform)
        {
            currentPosition = pointB.transform;
        }
    }

}
