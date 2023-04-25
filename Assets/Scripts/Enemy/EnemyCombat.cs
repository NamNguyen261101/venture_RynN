using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackDamage = 1;
    [SerializeField] private float attackRate = 2f;

    [SerializeField] private LayerMask enemyLayers;

    // Follow Player 
    [SerializeField] private Transform player;
    [SerializeField] private float minimumDistance = 4f;
    [SerializeField] private float moveSpeed = 2f;

    // Change Direction
    private bool isMovingLeft = true;

    // Cooldown
    [SerializeField] private float nextAttackTime;
    [SerializeField] private float attackCoolDown = 0;
    private float coolDownTimer = 2f;
    // 
    private bool isActive;
    public EnemyCombat instance;

    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        
        if (isActive)
        {
            Debug.Log("Run");
            FollowToPlayer();
        }
        
    }
    public void UpdateActionAttack()
    {
        Debug.Log("Attack");
        isActive = true;
        
    }

    public void DisableAttack()
    {
        isActive = false;
    }

    // Focus to player 
    // Focus player
    private void FollowToPlayer()
    {
        // Folow to minimdistance
        if (Vector2.Distance(transform.position, player.position) > minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        // Change direction 
        if (isMovingLeft)
        {
            if (transform.position.x > player.position.x)
            {
                transform.localScale = new Vector2((float)-0.5, (float)0.5);
            }
            else
            {
                isMovingLeft = false;
            }
        }

        else
        {
            if (transform.position.x > player.position.x)
            {
                transform.localScale = new Vector2((float)-0.5, (float)0.5);
            }
            else
            {
                isMovingLeft = true;
            }
        }

        // attack
        if (Time.time > attackCoolDown)
        {
            Debug.Log("hit to player");
            EnemyAttack();
            attackCoolDown = Time.time + coolDownTimer;
        } 
    }

    // enemy Attack
    private void EnemyAttack()
    {
        // Play an attack animation
        animator.SetTrigger("attack");

        // Detect enemies in Range of Attack
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D player in hit)
        {
            Debug.Log("hit player" + player.name);
            player.GetComponent<PlayerBehaviour>().PlayerTakeDmg(attackDamage);

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }

}
