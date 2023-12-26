using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class upgradeBar : MonoBehaviour
{
    
    public int[] upgradeCount;

    public GameObject[] balls;

    private Manager manager;
    private Player player;

    private int needKills = 10;

    public int nowUpgrade;
    
    private Color  disable = new(61/255f,60/255f,60/255f);
    private Color  active = new(255/255f,255/255f,255/255f);
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Manager>();
        transform.GetChild(nowUpgrade).GetComponent<Image>().color = active;
        upgradeCount = new int[9];
    }

    public void changeUpgrade(int num)
    {
        transform.GetChild(nowUpgrade).GetComponent<Image>().color = disable;

        nowUpgrade = num;
        
        transform.GetChild(num).GetComponent<Image>().color = active;
    }

    private void Update()
    {
        if (manager.kills == needKills)
        {
            player.PlaySound(player.sounds[4]);
            needKills += 10;
            var upgradeC = Random.Range(0, 9);
            upgradeCount[upgradeC] += 1;
            transform.GetChild(upgradeC).transform.GetChild(2).GetComponent<Text>().text = upgradeCount[upgradeC].ToString();
        }
    }
}
