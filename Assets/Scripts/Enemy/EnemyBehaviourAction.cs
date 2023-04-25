using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviourAction : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5;
    [SerializeField] private float currentHealth;

    [SerializeField] private HealthBarAll healthBarAll;
    private Animator anim;
    private bool dead;

   
    private void Start()
    {
       currentHealth = maxHealth;
       healthBarAll.SetMaxHealth(maxHealth);
       anim = GetComponent<Animator>();
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) 
        {
            TakeDamaged(2);
        }
    }

    public void TakeDamaged(float damage)
    {
        currentHealth -= damage;
        //Debug.Log("get damage from player ");
        anim.SetTrigger("hurt");
        
        // Hurt animation
        
        if (currentHealth > 0)
        {
           Debug.Log("Current health" + "  " + currentHealth);
           healthBarAll.SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
        {  
            Die();
        }


    }

    private void Die()
    {
        anim.SetTrigger("die");
        // cooldown after dead
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Touch");
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Deal Dmg");
            collision.gameObject.GetComponent<PlayerBehaviour>().PlayerTakeDmg(1);
            
        }
    }

}
