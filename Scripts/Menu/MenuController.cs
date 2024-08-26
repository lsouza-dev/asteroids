using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject credits;
    [SerializeField] private Toggle[] toggles;
    
    // Start is called before the first frame update
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }

    public void EndGame()
    {
        Application.Quit();
    }

}
