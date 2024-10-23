using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    string gameMode;
    private void Start()
    {
        gameMode = PlayerPrefs.GetString("gameMode");
        PlayerPrefs.SetInt("firstPlay", 0);
    }
    public void RestarGame()
    {
        if (gameMode.Equals("asteroids")) SceneManager.LoadScene("Asteroids");
        else if (gameMode.Equals("shooter"))
        {
            Boss.BOSSINDEX = 0;
            SceneManager.LoadScene("shooter");
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        PlayerPrefs.SetInt("firstGameplay",0);
        PlayerPrefs.SetInt("firsPlay", 0);
        Application.Quit();
    }
}
