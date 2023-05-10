using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentStats : MonoBehaviour
{
    public float currentHealth = 5;
    public int numCoins = 0;

    public static PermanentStats persist;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (persist ==  null ) // wanna start at diff levels
        {
            persist = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // gameobject.FindobjectbyType<>()
    // SceneManager.LoadScene
}
