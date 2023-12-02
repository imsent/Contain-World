using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
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
                //Cursor.visible = true;
                break;
            case true:
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                //Cursor.visible = false;
                break;
        }
    }
    public void quit()
    {
        SceneManager.LoadScene("Menu");
    }
    public void resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        //Cursor.visible = false;
    }
}
