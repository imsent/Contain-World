using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : Sounds
{
    public Sprite[] tutorials;

    public Vector2[] positions;

    public Image tutorial;

    public GameObject pauseMenu;

    public GameObject leftButton;

    public GameObject rightButton;

    private int currentIndex;    
    
    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    public void ChangeSlide(int index)
    {
        PlaySound(sounds[0]);
        currentIndex += index;
        switch (currentIndex)
        {
            case -1:
                SceneManager.LoadScene("Menu");
                break;
            case 11:
                PlayerPrefs.SetInt("firstStart", 1);
                SceneManager.LoadScene("Game");
                break;
            default:
                tutorial.sprite = tutorials[currentIndex];
                leftButton.transform.localPosition = positions[currentIndex];
                rightButton.transform.localPosition = new Vector2(positions[currentIndex].x + 201, positions[currentIndex].y);
                break;
        }
    }
    
    private void Pause()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
    public void quit()
    {
        PlaySound(sounds[0]);
        SceneManager.LoadScene("Menu");
    }
    public void resume()
    {
        PlaySound(sounds[0]);
        pauseMenu.SetActive(false);
    }
}
