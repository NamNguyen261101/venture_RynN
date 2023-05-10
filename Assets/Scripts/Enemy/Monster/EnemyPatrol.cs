using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float leftLimit = 0f;
    [SerializeField] private float rightLimit = 5f;

    private bool movingLeft = true;
    private Rigidbody2D rb;
    private Animator anim;

   

    // Attack AI
    private float distance;  // store the distance b/w enemy and player
    private bool isAttackMode;
    private bool isCooling; // Check if Enemy is cooling 
    private float intTimer;

    [SerializeField] private float attackRange; // Min distance for attack
    [SerializeField] private float damage; // Damage to enemy
    [SerializeField] private float timer; // Time cooldown for attack

    public EnemyPatrol instance;
    // public bool Active { get; set; }
    private bool isActive;
    private void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isActive)
        {
            Debug.Log("RunPatrol");
            EnemyMovement();
        }   
    }
    public void UpdateAction()
    {  
        isActive = true;
    }

    public void DisablePatrol()
    {
        isActive = false;
    }
    public void EnemyMovement()
    {
        anim.SetBool("isRunning", true);
        if (movingLeft)
        {
            if (transform.position.x >= leftLimit)
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                transform.localScale = new Vector2(-6, 6);
            }
            else
            {
                movingLeft = false;
            }
        }
        // Moving Right
        else
        {
            if (transform.position.x <= rightLimit)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

                transform.localScale = new Vector2(6, 6);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().PlayerTakeDmg(1);
           
        }
    }*/

    // Change AI to allow enemy to chance direction if colliders with anything that is not ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) // || !collision.gameObject.CompareTag("Player")
        {
            if (movingLeft)
            {
                movingLeft = false;
            } else
            {
                movingLeft = true;
            }
        }
    }

    
    

}
