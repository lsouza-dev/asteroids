using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameMode;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject credits;
    [SerializeField] private Toggle[] toggles;
    Toggle fullScreen;

    public int firstGameplay = 0;

    private void Start()
    {
        PlayerPrefs.SetInt("firstPlay", 0);
         
    }

    // Start is called before the first frame update

    public void Asteroids()
    {
        //firstGameplay = PlayerPrefs.GetInt("firstGameplay");
        firstGameplay = 1;
        if (firstGameplay == 0 )
        {
            SceneManager.LoadScene("CutScene");
            PlayerPrefs.SetString("gameMode", "asteroids");
        }
        else
        {
            PlayerPrefs.SetInt("firstPlay", 0);
            PlayerPrefs.SetString("gameMode", "asteroids");
            SceneManager.LoadScene("Asteroids");
        }
    }

    public void Shooter()
    {
        SceneManager.LoadScene("Shooter");
        PlayerPrefs.SetString("gameMode", "shooter");
    }

    public void OpenGameMode()
    {
        gameMode.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseGameMode()
    {
        gameMode.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("firstPlay", 0);
        PlayerPrefs.SetInt("firstGameplay", 0);
        Application.Quit();
    }
}
