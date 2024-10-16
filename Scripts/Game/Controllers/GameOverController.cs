using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    public void RestarGame()
    {
        PlayerPrefs.SetInt("firstPlay",0);
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        PlayerPrefs.SetInt("firstGameplay",0);
        Application.Quit();
    }
}
