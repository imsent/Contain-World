using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject settings;
    // Start is called before the first frame update
    public void quit()
    {
        Application.Quit();
    }
    
    public void playGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Settings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }
}
