using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public ScoreManager scoreManager;
    private Animator animator;
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    bool isOnGround = true;
    bool isMoving = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        HorizontalMovement(horizontal);
        
        VerticalMovement(vertical);

        // Crouch animation
        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            animator.SetBool("isCrouching", true);
            isMoving = false;
        }
        else if (Input.GetKeyUp(KeyCode.RightControl) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", false);
            isMoving = true;
        }
    }

    void PlayerMovementHorizontal(float horizontal) 
    { 
        Vector2 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DeathCheck")
        {
            animator.SetBool("isDead", true);
            isMoving = false;
        }
        else
        {
            animator.SetBool("isDead", false);
            isMoving = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DeathCheck")
        {
            Debug.Log("You failed!! Restarting the level.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void VerticalMovement(float vertical)
    {
        // Vertical Character movement
        if (vertical > 0 && isOnGround)
        {
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    void HorizontalMovement(float horizontal) 
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Horizontal character movement (Player_Idle -> Player_Run)
        Vector3 scale = transform.localScale;
        if (horizontal > 0 && isMoving)
        {
            scale.x = Mathf.Abs(scale.x);
            PlayerMovementHorizontal(horizontal);
        }
        else if (horizontal < 0 && isMoving)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
            PlayerMovementHorizontal(horizontal);
        }
        transform.localScale = scale;
    }

    public void PickupKey()
    {
        scoreManager.IncrementScore(10);
    }
}
