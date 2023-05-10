using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;

    [SerializeField] private float moveSpeed = 4.5f;

    private float dealDamage = 1f;

    private float timer;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

    }


    private void Update()
    {

        ArrowForce();

        timer += Time.deltaTime;
        if (timer > 1)
        {
            Destroy(gameObject);
        }
    }


    private void ArrowForce()
    {
        transform.position += transform.right * Time.deltaTime * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().PlayerTakeDmg(dealDamage);
            Debug.Log("Hit Player + deal damage " + collision.gameObject.name);
        }
        // Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        Destroy(gameObject);   
    }   


}
