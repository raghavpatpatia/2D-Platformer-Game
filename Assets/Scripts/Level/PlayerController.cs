using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] ScoreManager scoreManager;
    private HealthManager healthManager;
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] Transform checkpoint;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    private ParticleSystem jumpParticle;
    public bool isOnGround { get; private set;}
    private bool isMoving = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GameOverMenu.SetActive(false);
        healthManager = GetComponent<HealthManager>();
        jumpParticle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        bool jump = (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space));

        HorizontalMovement(horizontal);
                
        VerticalMovement(jump);

        Crouch();
        
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
        if (collision.gameObject.GetComponent<EnemyController>() != null && isOnGround)
        {
            DeathByCollision();
        }
    }

    public void DeathByCollision()
    {
        healthManager.health--;
        if (healthManager.health <= 0)
        {
            isMoving = false;
            animator.SetBool("isDead", true);
            SoundManager.Instance.Play(Sounds.PlayerHurt);
            StartCoroutine(ReloadLevelAfterAnimation());
        }
        else
        {
            SoundManager.Instance.Play(Sounds.PlayerDeath);
            animator.SetBool("isDead", true);
            StartCoroutine(PlayerEnemyCollision());
        }
        healthManager.UpdateHealth();
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
            isMoving = false;
            SoundManager.Instance.Play(Sounds.PlayerDeath);
            animator.SetBool("isDead", true);
            StartCoroutine(ReloadLevelAfterAnimation());
        }
    }

    private void Crouch()
    {
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

    private void VerticalMovement(bool jump)
    {
        // Vertical Character movement
        if (jump && isOnGround)
        {    
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("isJumping", true);
            SoundManager.Instance.Play(Sounds.PlayerJump);
            jumpParticle.Play();
        }
        else
        {   
            animator.SetBool("isJumping", false);
        }
    }

    private void HorizontalMovement(float horizontal) 
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
        SoundManager.Instance.Play(Sounds.KeyPickup);
        scoreManager.IncrementScore(10);
    }

    IEnumerator ReloadLevelAfterAnimation()
    {
        Debug.Log("You failed!! Restarting the level.");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        GameOverMenu.SetActive(true);   
    }
    
    IEnumerator PlayerEnemyCollision()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.transform.position = checkpoint.position;
        Physics2D.IgnoreLayerCollision(6, 7, false);
        animator.SetBool("isDead", false);
    }

}
