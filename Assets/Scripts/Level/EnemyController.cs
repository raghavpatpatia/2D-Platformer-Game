using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;
    private bool isAlive = true;
    private Animator animator;
    private bool isInRangeAnimationPlaying = false;
    private bool isDeathAnimationPlaying = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAlive)
        {
            EnemyMovement();
        }
    }

    private void EnemyMovement()
    {
        if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
            {
                FlipEnemy(true);
                patrolDestination = 1;
            }
        }

        else if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
            {
                FlipEnemy(false);
                patrolDestination = 0;
            }
        }
    }

    private void FlipEnemy(bool flip)
    {
        Vector3 scale = transform.localScale;
        if (flip)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (!flip)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null && collision.gameObject.GetComponent<PlayerController>().isOnGround == false)
        {
            isAlive = false;
            SoundManager.Instance.Play(Sounds.EnemyDeath);
            isDeathAnimationPlaying = true;
            animator.SetBool("isDead", true);
            StartCoroutine(WaitForAnimationFinish());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            SoundManager.Instance.Play(Sounds.EnemyAttack);
            Debug.Log("Player in range");
            isInRangeAnimationPlaying = true;
            animator.SetBool("inRange", true);
            StartCoroutine(WaitForAnimationFinish());
        }   
    }

    IEnumerator WaitForAnimationFinish()
    {
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);

        if (isDeathAnimationPlaying)
        {
            gameObject.SetActive(false);
        }
        else if (isInRangeAnimationPlaying)
        {
            animator.SetBool("inRange", false);
            isInRangeAnimationPlaying = false;
        }
    }
}
