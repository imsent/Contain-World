using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class enemySpawn : MonoBehaviour
{
    public GameObject[] Enemy;

    public Transform[] spawnPoint;

    public float startSpawnerInterval;

    private float spawnerInterval;

    private Manager manager;
    
    public float timerWave = 10;
    
    public Text timerText;

    public Text waveLvl;

    public Text killsText;
    
    public GameObject WaveTimer;
    
    public GameObject WaveKill;


    private GameObject spawnNow;

    private int chanceGolem = 5;

    private int chanceWisp = 45;

    private int chanceWizard = 50;

    private float countSpawn;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Manager>();
        countSpawn = manager.maxEnemy;
        killsText.text = "Осталось " + manager.maxEnemy + " врагов";
        spawnerInterval = startSpawnerInterval;
        waveLvl.text = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().lvl + " волна начнется через";
    }

    // Update is called once per frame
    void Update()
    {
        if (!(timerWave <= 0))
        {
            timerWave -= Time.deltaTime;
            timerText.text = Mathf.Round(timerWave).ToString();
            return;
        }
        if (WaveTimer.activeSelf)
        {
            WaveTimer.SetActive(false);
            WaveKill.SetActive(true);
        }

        if (manager.maxEnemy == 0)
        {
            killsText.text = "Соберите все трупы, чтобы продолжить";
            return;
        }
        killsText.text = "Осталось " + manager.maxEnemy + " врагов";
        if (countSpawn <= 0)
        {
            return;
        }
        spawnerInterval -= Time.deltaTime;
        if (!(spawnerInterval <= 0)) return;

        var random = Random.Range(0, 100);

        if (random >= 0 && random < chanceGolem)
        {
            spawnNow = Enemy[0];
        }else if (random >= chanceGolem && random < (chanceWisp + chanceGolem))
        {
            spawnNow = Enemy[1];
        }
        else
        {
            spawnNow = Enemy[2];
        }
        
        Instantiate(spawnNow, spawnPoint[Random.Range(0, spawnPoint.Length)].transform.position, Quaternion.identity);
        countSpawn--;
        spawnerInterval = startSpawnerInterval;
    }
    public void SelectWave(int lvl)
    {
        switch (lvl)
        {
            case 2:
                manager.maxEnemy = 30;
                chanceGolem = 10;
                chanceWisp = 40;
                chanceWizard = 50;
                break;
            case 3:
                manager.maxEnemy = 40;
                chanceGolem = 15;
                chanceWisp = 40;
                chanceWizard = 45;
                break;
            case 4:
                manager.maxEnemy = 50;
                chanceGolem = 20;
                chanceWisp = 40;
                chanceWizard = 40;
                break;
            case 5:
                manager.maxEnemy = 60;
                chanceGolem = 30;
                chanceWisp = 50;
                chanceWizard = 20;
                break;
        }

        countSpawn = manager.maxEnemy;
        timerWave = 10;
        WaveTimer.SetActive(true);
        killsText.text = "Осталось " + manager.maxEnemy + " врагов";
        WaveKill.SetActive(false);
        waveLvl.text = lvl + " волна начнется через";
    }
}
