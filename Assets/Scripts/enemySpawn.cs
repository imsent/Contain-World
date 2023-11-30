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

    public float maxEnemy = 20;
    
    public float timerWave = 60;
    
    public TextMeshProUGUI timerText;

    public TextMeshProUGUI waveLvl;

    public TextMeshProUGUI killsText;
    
    public GameObject WaveTimer;
    
    public GameObject WaveKill;
    // Start is called before the first frame update
    void Start()
    {
        killsText.text = "Осталось " + maxEnemy + " врагов";
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

        if (maxEnemy == 0)
        {
            killsText.text = "Соберите все трупы, чтобы продолжить";
            return;
        }
        spawnerInterval -= Time.deltaTime;
        if (!(spawnerInterval <= 0)) return;
        //Instantiate(Enemy, spawnPoint[Random.Range(0, spawnPoint.Length)].transform.position, Quaternion.identity);
        maxEnemy--;
        killsText.text = "Осталось " + maxEnemy + " врагов";
        spawnerInterval = startSpawnerInterval;
    }
    public void SelectWave(int lvl)
    {
        
        maxEnemy = lvl switch
        {
            2 => 30,
            3 => 40,
            4 => 50,
            5 => 60,
            _ => maxEnemy
        };
        timerWave = 10;
        WaveTimer.SetActive(true);
        killsText.text = "Осталось " + maxEnemy + " врагов";
        WaveKill.SetActive(false);
        waveLvl.text = lvl + " волна начнется через";
    }
}
