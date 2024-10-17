using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject credits;
    [SerializeField] private Toggle[] toggles;
    Toggle fullScreen;

    public int firstGameplay = 0;


    // Start is called before the first frame update

    public void Play()
    {
        firstGameplay = PlayerPrefs.GetInt("firstGameplay");
        if (firstGameplay == 0)
        {
            SceneManager.LoadScene("CutScene");
        }
        else
        {
            PlayerPrefs.SetInt("firstPlay", 0);
            SceneManager.LoadScene("Game");
        }
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("firstGameplay", 0);
        Application.Quit();
    }
}
