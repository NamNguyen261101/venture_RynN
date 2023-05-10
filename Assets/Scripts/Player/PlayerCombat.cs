using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat instance;
    public EnemyBehaviourAction enemyAction;

    [SerializeField] private Animator myAnim;
    private bool isAttacking = false;

    [SerializeField] private GameObject attackPoint;
    [SerializeField] private float attackRange = 0.5f; // radius
    [SerializeField] private LayerMask enemyLayers;

    public float attackDamage = 2;

    [SerializeField] private ProjectileBehaviour projectileBehaviour;
    [SerializeField] private Transform lauchOffset;

    private float timer;

    private void Start()
    {
        instance= this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }

        if (Input.GetButtonDown("Fire1"))
        {
             
            // Debug.Log("Fireball");
            Instantiate(projectileBehaviour, lauchOffset.position, transform.rotation);
        }
    }

    private void Attack()
    {
        // Play an attack animation
        myAnim.SetTrigger("attack");

        // Detect enemies in Range of Attack
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hit)
        {
            Debug.Log("hit enemy" + enemy.name);
            enemy.GetComponent<EnemyBehaviourAction>().TakeDamaged(attackDamage);
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
