using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetected : MonoBehaviour
{

    [SerializeField]
    public bool PlayerDetected { get; private set; }

    public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;

    [Header("OverlapBox parameters")]
    [SerializeField] private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;

    public float detectionDelay = 0.3f;

    public LayerMask detectorLayerMask;

    [Header("Gizmo parameter")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColr = Color.red;
    public bool showGizmos = true;

    private GameObject target;

    [SerializeField] public GameObject hotZone;
    public bool PlayerInArea { get; private set; }
    private void Update()
    {
       //  PerformDetection();
    }

    /*public void PerformDetection()
    {
        Debug.Log("Player get in");
        Collider2D collider = Physics2D.OverlapBox((Vector2) detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);

        if (collider != null )
        {
            target = collider.gameObject;
        } else
        {
            target = null;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hotZone = collision.gameObject;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos & detectorOrigin != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (PlayerDetected)
            {
                Gizmos.color = gizmoDetectedColr;

            }

            Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
        }
    }


}
