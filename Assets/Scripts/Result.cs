using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject win;

    public GameObject lose;

    public GameObject UI;
    // Start is called before the first frame update

    public void resultGame(bool result)
    {
        Time.timeScale = 0f;
        UI.SetActive(false);
        if (result)
        {
            win.SetActive(true);
        }
        else
        {
            lose.SetActive(true);
        }
    }
}
