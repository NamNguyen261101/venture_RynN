using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private Transform player;
    [SerializeField] private float agroRange;

    // Follow Player 
    [SerializeField] private float moveSpeed = 2f;
    // Change Direction
    private bool isMovingLeft = true;

    // attack
    [SerializeField] private Transform attackTransform;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float damageAmount;
    [SerializeField] private float timeBtwAttacks = 2f;


    private RaycastHit2D[] hits;
    private float attackTimeCounter;

    private bool isActive = false;
    private EnemyCombat instance;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackTimeCounter = timeBtwAttacks;
        instance = this;
    }


    void Update()
    {
        if (!isActive)
        {
            DistanceToPlayer();
        }

    }
    public void UpdateActionAttack()
    {
        isActive = true;
    }
    public void DisableActionAttack()
    {
        isActive = false;
    }
    private void DistanceToPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > agroRange)
        {
            // (Move)
            StopChasingPlayer();
            StopAttack();
        }
        else if (distanceToPlayer < agroRange)
        {
            // Stop chasing player
            ChasePlayer();
            if (attackTimeCounter >= timeBtwAttacks)
            {
                // Reset the counter
                attackTimeCounter = 0;
                Attack();
            }
            attackTimeCounter += Time.deltaTime;
        }
    }

    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            // enemy is the left side of the player, move right
            rb.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2((float)0.3, (float)0.3);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2((float)-0.3, (float)0.3); ;
        }
    }

    private void StopChasingPlayer()
    {
        rb.velocity = new Vector2(0, 0);
    }

    // Attack
    private void Attack()
    {
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);
        for (int i = 0; i < hits.Length; i++)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);
            PlayerBehaviour playerHealth = hits[i].collider.gameObject.GetComponent<PlayerBehaviour>();
            if (playerHealth != null)
            {
                // Apply damage
                playerHealth.PlayerTakeDmg(damageAmount);
            }
        }
    }
    // Stop Attack
    private void StopAttack()
    {
        animator.SetBool("isAttacking", false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

}
