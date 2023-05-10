using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementInputDirection;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;


    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer playerSprite;
  
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float distanceBetweenImages;
    [SerializeField] private float dashCoolDown;
    [SerializeField] private float hurtForce = 15f;

    [SerializeField] private int amountOfJumps = 1;
    private int amountOfJumpsLeft;
    private int facingDirection = 1;

    private bool isFacingRight = true;
    private bool isRunning;
    private bool isGround;
    private bool canJump;
    private bool canMove;
    private bool canFlip = true;
    //dash
    private bool isDashing;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask whatIsGround;

    // State check hurt
    private enum State
    {
        isHurt,
        notHurt
    }

    private State state = State.notHurt;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckDirection();
        UpdateAnimattions();
        CheckAllowToJump();
        CheckDash(); // Dash Not Working
    }

    private void FixedUpdate()
    {
        if (state != State.isHurt)
        {
            ApplyMovement();
            CheckSurroundings();
        }
        
    }
    // Check Input
    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                AttemptToDash();
            }
        }
    }

    private void ApplyMovement()
    {
        #region Move & Run

        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);

        #endregion
    }
    // check Direction
    private void CheckDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        } else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) >= 0.01f) 
        {
            isRunning = true;
        } else
        {
            isRunning = false;
        }
    }

    private void UpdateAnimattions()
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isGrounded", isGround);
        animator.SetFloat("yVelocity", rb.velocity.y);
    }
    // Facing Direction Attack 
    public int GetFacingDirection()
    {
        return facingDirection;
    }

    // FLip
    public void DisableFlip()
    {
        // Debug.Log("DisableFlip");
        canFlip = false;
    }

    public void EnableFlip()
    {
        canFlip = true;
    }
    private void Flip()
    {
        if (canFlip)
        {
            // Debug.Log("Facing oke");
            isFacingRight = !isFacingRight;
            /*Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;*/
            transform.Rotate(0f, 180f, 0f);
        }
    }
    // Jump
    private void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
    }
    
    // Dash
    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
        PlayerAfterImagePool.instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }


    // Check
    private void CheckSurroundings()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    
    private void CheckAllowToJump()
    {
        if (isGround && rb.velocity.y <= 0)
        {
            
            amountOfJumpsLeft = amountOfJumps;
        } 

        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        } else
        {
            canJump= true;
        }
    }
    // Try to dash (not work)
    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, rb.velocity.y);
                dashTime -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }  
        }
    }

    // Check Jump Hit Enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }

    // Bounces Sprite
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            state = State.isHurt;

            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                StartCoroutine(BounceLeft());
            }
            else
            {
                StartCoroutine(BounceRight());
            }

        }
    }
    
    private IEnumerator BounceLeft()
    {
        playerSprite.color = Color.red;
        rb.velocity = new Vector2(15, rb.velocity.y); // hurtForce = x
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = Color.white;
        state = State.notHurt;
    }

    private IEnumerator BounceRight()
    {
        playerSprite.color = Color.red;
        rb.velocity = new Vector2(-15, rb.velocity.y);
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = Color.white;
        state = State.notHurt;
    }


    // Gizmos 

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
