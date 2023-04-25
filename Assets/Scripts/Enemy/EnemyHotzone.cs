using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHotzone : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerInArea { get; private set; }
    public Transform Player { get; private set; }

    public UnityEvent pageEnter;
    public UnityEvent pageExit;

    // patrol first 

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInArea = true;
            Player = collision.gameObject.transform;
            pageEnter?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInArea = false;
            Player = null;
            pageExit?.Invoke();
        }
    }

}
