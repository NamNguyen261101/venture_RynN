using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverPause : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPauseScreen;
    public static bool isGamePaused = false;
    [SerializeField] private GameObject gameOverText;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        gameOverPauseScreen.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1;
        
    }

    public void Pause()
    {
        gameOverPauseScreen.SetActive(true);
        isGamePaused = true;
        if (isGamePaused == false)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }


    public void RestartGame()
    {
        //SceneManager.LoadScene("Level1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;

    }

    public void Quit()
    {
        Application.Quit();
    }
}
