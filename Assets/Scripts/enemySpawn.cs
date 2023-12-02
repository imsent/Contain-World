using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public GameObject Enemy;

    public Transform[] spawnPoint;

    public float startSpawnerInterval;

    private float spawnerInterval;

    private Manager manager;
    
    public float timerWave = 60;
    
    public TextMeshProUGUI timerText;

    public TextMeshProUGUI waveLvl;

    public TextMeshProUGUI killsText;
    
    public GameObject WaveTimer;
    
    public GameObject WaveKill;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Manager>();
        killsText.text = "Осталось " + manager.maxEnemy + " врагов";
        spawnerInterval = startSpawnerInterval;
        waveLvl.text = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().lvl + " волна начнется через";
    }

    // Update is called once per frame
    void Update()
    {
        killsText.text = "Осталось " + manager.maxEnemy + " врагов";
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
        spawnerInterval -= Time.deltaTime;
        if (!(spawnerInterval <= 0)) return;
        Instantiate(Enemy, spawnPoint[Random.Range(0, spawnPoint.Length)].transform.position, Quaternion.identity);
        spawnerInterval = startSpawnerInterval;
    }
    public void SelectWave(int lvl)
    {
        
        manager.maxEnemy = lvl switch
        {
            2 => 30,
            3 => 40,
            4 => 50,
            5 => 60,
            _ => manager.maxEnemy
        };
        timerWave = 10;
        WaveTimer.SetActive(true);
        killsText.text = "Осталось " + manager.maxEnemy + " врагов";
        WaveKill.SetActive(false);
        waveLvl.text = lvl + " волна начнется через";
    }
}
