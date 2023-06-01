using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    void VerticalMovement(float vertical)
    {
        // Vertical Character movement
        if (vertical > 0 && isOnGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            animator.SetBool("isJumping", true);
            isOnGround = false;
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
}
