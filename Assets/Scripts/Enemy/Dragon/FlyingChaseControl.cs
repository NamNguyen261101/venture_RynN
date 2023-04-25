using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingChaseControl : MonoBehaviour
{
    public FLyingEnemy[] flyEnemies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (FLyingEnemy enemy in flyEnemies)
            {
                enemy.chase = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (FLyingEnemy enemy in flyEnemies)
            {
                enemy.chase = false;
            }
        }
    }
}
