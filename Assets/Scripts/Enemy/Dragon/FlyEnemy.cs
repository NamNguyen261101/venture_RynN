using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float circleRadius;
    [SerializeField] private float dirX = 1;
    [SerializeField] private float dirY = 0.25f;

    private Rigidbody2D flyEenemy;

    [SerializeField] private GameObject rightCheck;
    [SerializeField] private GameObject roofCheck;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool isFacingRight = true;
    private bool groundTouch;
    private bool roofTouch;
    private bool rigthtTouch;

    
    void Start()
    {
        flyEenemy = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        flyEenemy.velocity = new Vector2(dirX, dirY) * speed * Time.deltaTime;
        HitDirection();
    }

    private void HitDirection()
    {
        // rightTouch = Physics2D.OverlapCircle(rightCheck.transform.position, circleRadius, groundLayer);
         
    }
}
