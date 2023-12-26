using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : Sounds
{
    public GameObject UI;
    
    private Manager manager;
    // Start is called before the first frame update

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Manager>();
    }

    public void resultGame(bool result)
    {
        var info = transform.GetChild(2);
        info.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = manager.kills + " врагов";
        info.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = manager.towerCountPlace + " башен";
        info.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = manager.zonePercent*100 + "% заражено";
        info.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = manager.upCount + " бустов";
        Time.timeScale = 0f;
        UI.SetActive(false);
        if (result)
        {
            PlaySound(sounds[0]);
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            
            PlaySound(sounds[1]);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        info.gameObject.SetActive(true);
    }
}
