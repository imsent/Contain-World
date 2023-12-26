using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Sounds
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        switch (pauseMenu.activeSelf)
        {
            case false when Time.timeScale == 1f:
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                break;
            case true:
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                break;
        }
    }
    public void quit()
    {
        PlaySound(sounds[0]);
        SceneManager.LoadScene("Menu");
    }
    public void resume()
    {
        PlaySound(sounds[0]);
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
