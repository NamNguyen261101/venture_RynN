using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnemyControlScript : MonoBehaviour
{
    // Control 2 script
    // Calling if play hit into hotzone then attack
    // othherwise just patrolling
    // Observer pattern 
    public EnemyCombat enemyAttack;
    public EnemyPatrol enemyPatrol;
    public EnemyHotzone enemyHotzone;



    public void ControlEvents()
    {
      
    }

    // Event
    public void EnemyCombat()
    {
        if (enemyHotzone.PlayerInArea == true   )
        {
            Debug.Log("Enemy combat");
            //enemyAttack.Invoke("UpdateActionAttack", 0);
        }
    }

    public void EnemyPatrol()
    {
        if (enemyHotzone.PlayerInArea == false)
        {
            // enemyAttack.Invoke("UpdateAction", 0);
            
        }
    }

    // IAP
    
    private bool isHittingHotzone;

    // unity event
}
