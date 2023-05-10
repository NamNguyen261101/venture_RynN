using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSkill : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(transform.right * transform.localScale.x * speed * Time.deltaTime); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            return;
        }
        // Trigger the custom action on the other object if it exits
        /*if (collision.GetComponent<SplashAction>())
        {
            collision.GetComponent<SplashAction>().Action();
        }*/

        // Destroy
        Destroy(gameObject);
    }
}
