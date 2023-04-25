using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreCoinOnBoard : MonoBehaviour
{
    public static ScoreCoinOnBoard instance;

    [SerializeField] private Text scoreText;
    private int numCoins;

    private void Start()
    {
        numCoins = 0;
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int coinValue)
    {
        PermanentStats.persist.numCoins += coinValue;
        scoreText.text = "X " + " " + PermanentStats.persist.numCoins.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coins")
        {
            PermanentStats.persist.numCoins += 1;
            Destroy(collision.gameObject);
            scoreText.text =  " " + PermanentStats.persist.numCoins.ToString();
        }
    }
}
