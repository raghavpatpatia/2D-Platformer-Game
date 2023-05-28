using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isCrouching", false);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Horizontal character movement (Player_Idle -> Player_Run)
        Vector3 scale = transform.localScale;
        if (horizontal > 0) 
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else if (horizontal < 0) 
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        // Vertical Character movement
        if (vertical > 0) 
        {
            animator.SetBool("isJumping", true);
        }
        else if (vertical <= 0) 
        {
            animator.SetBool("isJumping", false);
        }

        // Crouch animation
        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            animator.SetBool("isCrouching", true);
        }
        else if (Input.GetKeyUp(KeyCode.RightControl) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", false);
        }
    }
}
