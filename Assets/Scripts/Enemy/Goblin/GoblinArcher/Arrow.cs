using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.5f;

    private Rigidbody2D rb;

   
    private void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ArrowSpeed();
    }

    private void ArrowSpeed()
    {
        
       
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
       
            Debug.Log("Hit");
            Destroy(gameObject);

    }

}
