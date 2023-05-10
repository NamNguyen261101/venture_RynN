using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5;
    private float currentHealth;
    public HealthBarAll healthBar;
    private Animator anim;
    private bool dead;

    [SerializeField] private GameObject gameOverScreen = null;
    [SerializeField] private GameObject gameOverText = null;
    [SerializeField] private GameObject finishScreen = null;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.T))
       {
            PlayerTakeDmg(2);
       }

       if (currentHealth <= 0 )
       {
            gameOverScreen.SetActive(true);
            gameOverText.SetActive(true);
            Time.timeScale = 0;
       } else 
       { 
            if (gameOverScreen.activeInHierarchy == true || finishScreen.activeInHierarchy == true)
            {
                Time.timeScale = 0;
            } else
            {
                Time.timeScale = 1;
            }
       }
    }

    public void PlayerTakeDmg(float dmg)
    {

        PermanentStats.persist.currentHealth -= dmg;
        //currentHealth -= dmg;
        if (PermanentStats.persist.currentHealth > 0 )
        // if (currentHealth > 0)
        {
            // player hurt
            anim.SetTrigger("hurt");
            healthBar.SetHealth(PermanentStats.persist.currentHealth);
        }
        else if (PermanentStats.persist.currentHealth <= 0)
        {
            Die();
        } 
  
    }

    public void PlayerHeal(float heal)
    {
        PermanentStats.persist.currentHealth += heal;
        healthBar.SetHealth(PermanentStats.persist.currentHealth);
    }

    private void Die()
    {
        Debug.Log("Die");
        anim.SetTrigger("die");
        // Player
        GetComponent<PlayerController>().enabled = false;
        gameOverScreen.SetActive(true);
        gameOverText.SetActive(true);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeathPlace"))
        {
            Die();
        }
    }*/
}
