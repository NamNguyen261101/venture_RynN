using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform rayCast;
    [SerializeField] private LayerMask rayCastMask;
    [SerializeField] private float rayCastLength;
    [SerializeField] private float attackDistance; // Min distance to attack
    [SerializeField] private float moveSpeed; 
    [SerializeField] private float timer; // time for cooldown attacks
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;

    private RaycastHit2D hit;
    private Transform target;
    [SerializeField] private Animator anim;
    private float distance; // distance between enemy and player
    private bool isAttackMode;
    private bool inRange; // Check if player is inRange
    private bool cooling; // Check if enemy is cooling  
    private float intTimer;

    private void Awake()
    {
        SelectTarget();
        intTimer = timer; // inital value of timer
       
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!isAttackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            SelectTarget();
        }
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, rayCastMask);
            RaycastInRange();
        }

        // When player is detected
        if (hit.collider != null)
        {
            EnemyFollow();
        } else if (hit.collider == null)
        {
            inRange = false;
        }

        if (inRange== false) 
        {
           //  anim.SetBool("canWalk", false);
            StopAttack();
        }
    }

    private void EnemyFollow()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            
            StopAttack();
        } else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            CoolDown();
            Debug.Log("Cooling Down " + timer);
            anim.SetBool("Attack", false);
        }
    }

    private void Move()
    {
        anim.SetBool("canWalk", true); 
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
    // PlayerInsight
    private void RaycastInRange()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.yellow);
        } 
        if (distance < attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.transform;
            inRange = true;
            ChangeDirection();
        }
    }
    // Attack
    private void Attack()
    {
        timer = intTimer; // Reset timer when player enter attack range
        isAttackMode = true;  // Check if enemy can still attack or not

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    // CoolDown
    private void CoolDown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && isAttackMode)
        {
            cooling = false;
            timer = intTimer; 
        }
    }

    // Stop Attack
    private void StopAttack()
    {
        cooling = false;
        isAttackMode = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    // Check limit
    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    // Select Target 
    private void SelectTarget()
    {
        float distanceToLeft =  Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        } else
        {
            target = rightLimit;
        }

        ChangeDirection();
    }
    private void ChangeDirection()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        } else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

}
