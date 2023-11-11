using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public GameObject stone;
    public GameObject tree;
    public int maxEnemy = 5;

    public float timeSpawn = 2f;
    private float timer;

    public float distance = 3;
    
    private void Start()
    {
        timer = timeSpawn;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (!(timer <= 0)) return;
        timer = timeSpawn;
        Instantiate(stone, Random.insideUnitCircle * distance, Quaternion.identity, transform);
        Instantiate(tree, Random.insideUnitCircle * distance, Quaternion.identity, transform);
    }
}
