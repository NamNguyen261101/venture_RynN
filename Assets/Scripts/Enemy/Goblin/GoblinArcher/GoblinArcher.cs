using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinArcher : MonoBehaviour
{
    // Attack Parameter
    [Header("Attack Parameters")]
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    [Header("Attack Parameters")]
    [SerializeField] private Transform arrowPoint;
    [SerializeField] private GameObject[] arrows;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider2D;

    [Header("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;

    // References
    private Animator anim;
    private PlayerBehaviour playerBehaviour;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
    }
    private void Update()
    {
        coolDownTimer += Time.deltaTime;
    }

    private void ArcherAttack()
    {
        // Attack only when player in sight?
        if (PlayerInSight())
        {
            if (coolDownTimer >= attackCoolDown)
            {
                coolDownTimer = 0;
                anim.SetBool("archerAttack", true);
            }
        }
    }

    private void RangedAttack()
    {
        coolDownTimer = 0;
        // Shoot
        arrows[0].transform.position = arrowPoint.position;


    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                           new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z),
                           0, Vector2.left, 0, playerLayer);
        
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                           new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z)
                           );
    }

}
