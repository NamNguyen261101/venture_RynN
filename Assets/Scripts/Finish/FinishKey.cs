using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishKey : MonoBehaviour
{
    [SerializeField] private GameObject finishScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            finishScreen.SetActive(true);
            // Time.timeScale = 0;
        }
    }
}
