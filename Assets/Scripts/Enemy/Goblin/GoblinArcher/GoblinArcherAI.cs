using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinArcherAI : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private Transform archerPoint;
    [SerializeField] private Transform gun;

    [SerializeField] private Transform player;

    [SerializeField] private float followPlayerRange;
    private bool inRange;
    [SerializeField] private float attackRange;

    [SerializeField] public float startTimeBtwnShots;
    private float timeBtwnShots;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckIfTimeToFire();
    }


    private void GetDirectionFollow()
    {
      
    }


    private void CheckIfTimeToFire()
    {
   
            Instantiate(arrowPrefab, transform.position, Quaternion.identity);
         
    }


    
}
