using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private bool isLooted = false;

    [SerializeField] private Sprite closeChest, openChest;

    [SerializeField] private SpriteRenderer spriteRerender;

    [SerializeField] private GameObject[] tresureItems;

    private float treasureAmount;

    [SerializeField] private float treasureSpread;

    float minAmount = 0, maxAmount = 10;

    private void Start()
    {
        spriteRerender.sprite = closeChest;
    }
    private void OnDestruction()
    {

        treasureAmount = Random.RandomRange(minAmount, maxAmount);

        if (isLooted)
        {
            for (int i =0; i < treasureAmount; i++)
            {
                int index = Random.Range(0, tresureItems.Length);
                Vector3 newRotation = new Vector3(0f, 0f, Random.Range(-treasureSpread, treasureSpread));
                Instantiate(tresureItems[index], transform.position, Quaternion.Euler(newRotation));
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLooted)
        {
            // Debug.Log("Loot open already");
            return;
        }
        isLooted = true;
        OnDestruction();

        spriteRerender.sprite = openChest;
    }
}
