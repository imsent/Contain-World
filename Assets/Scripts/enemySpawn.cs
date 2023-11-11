using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public GameObject Enemy;

    public Transform[] spawnPoint;

    public float startSpawnerInterval;

    private float spawnerInterval;

    private int randPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnerInterval = startSpawnerInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerInterval <= 0)
        {
            randPoint = Random.Range(0, spawnPoint.Length);
            Instantiate(Enemy, spawnPoint[randPoint].transform.position, Quaternion.identity);
            spawnerInterval = startSpawnerInterval;
        }
        else
        {
            spawnerInterval -= Time.deltaTime;
        }
    }
}
