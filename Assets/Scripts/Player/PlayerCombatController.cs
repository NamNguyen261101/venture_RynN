using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] 
    private bool combatEnabled;
    [SerializeField] 
    private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField] 
    private Transform attack1HitBoxPos;
    [SerializeField] 
    private LayerMask whatIsDamageable;


    private bool gotInput, isAttacking, isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity; // always ready to attack from the start

    private Animator animator;

    private bool isHit;
    private bool isCoolDown;


    [SerializeField] private EnemyBehaviourAction instance;
    [SerializeField] private float damage;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("canAttack", combatEnabled);
    }
    private void Update()
    {
        CheckCompatInput();
        CheckAttacks();
    }
    private void CheckCompatInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            /*Debug.Log("isAttacking");*/
            if (combatEnabled)
            {
                // 
                gotInput= true;
                lastInputTime = Time.time;
            }
        }
    }

    // Check
    private void CheckAttacks()
    {
        if (gotInput)
        {    
            // attack 1
            if (!isAttacking)
            {
                gotInput = true;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                animator.SetBool("attack1", true);
                animator.SetBool("firstAttack", isFirstAttack);
                animator.SetBool("isAttacking", isAttacking);
            }    
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            // wait for new input
            gotInput = false;
        }
    }

    
   

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }

    
}
