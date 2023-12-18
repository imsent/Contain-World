using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public List<EnemyNear> EnemyList = new();

    public int kills;

    public int towerCountPlace;

    public float zonePercent;

    public int upCount;

    public float maxEnemy = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEnemy(EnemyNear enemy)
    {
        EnemyList.Add(enemy);
    }
    public void RemoveEnemy(EnemyNear enemy)
    {
        EnemyList.Remove(enemy);
    }
}
