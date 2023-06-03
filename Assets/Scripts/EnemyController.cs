using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    private void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        Vector3 scale = transform.localScale;
        if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
            {
                scale.x = -1f * Mathf.Abs(scale.x);
                patrolDestination = 1;
            }
        }

        else if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
            {
                scale.x = Mathf.Abs(scale.x);
                patrolDestination = 0;
            }
        }
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null && collision.gameObject.GetComponent<PlayerController>().isOnGround == false)
        {
            gameObject.GetComponent<Animator>().SetBool("isDead", true);
            gameObject.SetActive(false);
        }
    }
}
