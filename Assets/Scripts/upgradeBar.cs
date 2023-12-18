using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeBar : MonoBehaviour
{
    public GameObject[] upgrade;

    private Player player;
    
    private Color  disable = new(61/255f,60/255f,60/255f);
    private Color  active = new(156/255f,156/255f,156/255f);
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        upgrade[player.nowUpgrade].GetComponent<Image>().color = active;
    }

    public void changeUpgrade(int num)
    {
        upgrade[player.nowUpgrade].GetComponent<Image>().color = disable;

        player.nowUpgrade = num;
        
        upgrade[num].GetComponent<Image>().color = active;
    }
}
