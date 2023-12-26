using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : Sounds
{
    public GameObject menu;
    public GameObject settings;
    // Start is called before the first frame update
    public void quit()
    {
        PlaySound(sounds[0]);
        Application.Quit();
    }
    
    public void playGame()
    {
        PlaySound(sounds[0]);
        SceneManager.LoadScene("Game");
    }

    public void Switch(bool choice)
    {
        PlaySound(sounds[0]);
        menu.SetActive(!choice);
        settings.SetActive(choice);
    }
}
