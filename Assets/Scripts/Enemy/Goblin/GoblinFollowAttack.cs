    using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GoblinFollowAttack : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    // Follow Player 
    [SerializeField] private Transform player;
    [SerializeField] private float minimumDistance = 2f;
    [SerializeField] private float moveSpeed = 2f;
    // Change Direction
    private bool isMovingLeft = true;

    // attack variables
    [SerializeField] private float attackDistance; // Minimum distance for attack
    [SerializeField] private float timer; // timer for cooldown between attacks

    private float distance;
    private bool isAttackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    void Start()
    {
        animator = GetComponent<Animator>();
        intTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        FollowToPlayer();
    }

    private void FollowToPlayer()
    {
        // Folow to mindistance
        ComeToAttackPoint();
        // Change direction 
        UpdateDirection();
        // Attack into a point
        Attacking();
    }

    private void ComeToAttackPoint()
    {

        if (IsCloseToPlayer())
        {
            Debug.Log("walk");
            animator.SetBool("isMoving", true);
            var dir = (player.position - transform.position).normalized;
            transform.position = transform.position + (dir * moveSpeed * Time.deltaTime);
        }
    }

    private bool IsCloseToPlayer()
    {
        return Vector2.Distance(transform.position, player.position) > minimumDistance;
    }

    private void UpdateDirection()
    {
        if (isMovingLeft)
        {
            if (transform.position.x > player.position.x)
            {
                transform.localScale = new Vector2((float)-0.3, (float)0.3);
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
                transform.localScale = new Vector2((float)-0.3, (float)0.3);
            }
            else
            {
                isMovingLeft = true;
            }
        }
    }
    private void CoolDownToAttackPlayer()
    {
      
    }
    // Attack using collider
    private void Attacking()
    {
        Debug.Log("EnemyAttack");
        // Play an attack animation
        animator.SetBool("isMoving", false);
        animator.SetTrigger("attack");
    }
}
