using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private static int selectedLevel;
    [SerializeField] private int level;
    [SerializeField] private Text levelText;

    void Start()
    {
        levelText.text = level.ToString();
    }

    public void OpenScene()
    {
        selectedLevel = level;
        SceneManager.LoadScene("Level" + level.ToString());
    }
}
