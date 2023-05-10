using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 4.5f;
    [SerializeField] private float attackDamage = 1f;
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {

        ProjectileSpeed();
    }

    private void ProjectileSpeed()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void SkillDealDamaged()
    {
        // play attack animations
        anim.SetTrigger("SkillEffect");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        // collision.GetComponent<EnemyBehaviourAction>().TakeDamaged(attackDamage);
        EnemyBehaviourAction enemyHealth = GetComponent<EnemyBehaviourAction>();
        if (enemyHealth != null )
        {
            enemyHealth.TakeDamaged(attackDamage);
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        Destroy(gameObject);
    }
}
