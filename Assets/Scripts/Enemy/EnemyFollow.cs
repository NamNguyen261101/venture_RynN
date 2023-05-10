using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private Transform player;
    [SerializeField] private float minimumDistance;
    [SerializeField] private float moveSpeed;

    // Change direction
    private bool isMovingLeft = true;

    // Attack Ai
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackDamage = 1;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private LayerMask enemyLayers;
    // Cooldown
    private float coolDownTimer = 1f;
    


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        coolDownTimer = Time.deltaTime;
        FollowToPlayer();
    }

    private void FollowToPlayer()
    {
        Debug.Log("Follow");
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
            } else
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
        if (attackCoolDown > Time.time)
        {
            
            // attackCoolDown = Time.time + coolDownTimer;
            attackCoolDown = coolDownTimer - Time.time;
            EnemyAttack();
        }
        

    }

    private void EnemyAttack()
    {
        // animation
        anim.SetTrigger("attack");

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
