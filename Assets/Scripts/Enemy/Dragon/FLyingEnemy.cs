using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLyingEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    public bool chase = false;
    [SerializeField] private Transform startingPoint;

    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        if (chase == true)
        {
            FlyingChase();
        } else
        {
            ReturnStartPoint();
        }
        FlyFlip();
    }

    private void FlyingChase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void FlyFlip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }
}
