using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    [SerializeField]
    private Transform
           castPos;

    [SerializeField]
    private float
            moveSpeed = 3f,
            baseCastPosDistance;

    private string
            facingDirection;

    private Rigidbody2D
            aliveRb;
    private Animator
            anim;
    private Vector3
            baseScale;
    const string RIGHT = "right";
    const string LEFT = "left";

    private bool isActive = false;
    private MonsterMoving instance;
    void Start()
    {
        facingDirection = RIGHT;
        baseScale = transform.localScale;
        aliveRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        instance = this;
        // isActive = true;
        UpdateActionPatrol();
            ApplingPatrol();
       /* if (isActive == true)
        {
            ApplingPatrol();
        }*/
    }


    public void Update()
    {
       if (!isActive)
        {
            ApplingPatrol();
        }
    }

    public void UpdateActionPatrol()
    {
        isActive = true;
       
    }
    public void DisableActionPatrol()
    {
        isActive = false;
    }


    // Appling Into Patrol
    private void ApplingPatrol()
    {
        GoblinApplyMovement();

        if (IsHittingWall() || IsNearEdge())
        {
            Debug.Log("touch");
            if (facingDirection == LEFT)
            {
                ChangingDirection(RIGHT);
            }
            else if (facingDirection == RIGHT)
            {
                ChangingDirection(LEFT);
            }
        }
    }

    // Moving 
    private void GoblinApplyMovement()
    {
        anim.SetBool("isMoving", true);
        float velocityX = moveSpeed;

        if (facingDirection == LEFT) 
        {
            velocityX = -moveSpeed;
        }
        aliveRb.velocity = new Vector2(velocityX, aliveRb.velocity.y);
    }

    // Flip
    private void ChangingDirection(string newDirection)
    {
        anim.SetBool("isMoving", false);
        
        Vector3 newScale = baseScale;
        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else if (newDirection == RIGHT)
        {
            newScale.x = baseScale.x;
        }
        transform.localScale = newScale;
      
        facingDirection = newDirection;
    }

    // Check if hitting wall
    private bool IsHittingWall()
    {
        bool isHit = false;

        float castDist;

        // Define cast distance left or right
        if (facingDirection == LEFT)
        {
            castDist = -baseCastPosDistance;
        }
        else
        {
            castDist = baseCastPosDistance;
        }
        // determine target based by cast distance
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground"))) // 1 << LayerMask.NameToLayer
        {
            isHit = true;
        }
        else
        {
            isHit = false;
        }

        return isHit;
    }

    // Check if nearEdge
    private bool IsNearEdge()
    {
        bool isHitEdge = true;

        float castDist = baseCastPosDistance;

        // determine target based by cast distance
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground"))) // 1 << LayerMask.NameToLayer
        {
            isHitEdge = false;
        }
        else
        {
            isHitEdge = true;
        }

        return isHitEdge;
    }
}
