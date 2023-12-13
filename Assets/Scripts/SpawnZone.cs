using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public Grid grid;
    
    public GameObject stone;
    public GameObject tree;
    public int maxEnemy = 5;

    public float timeSpawn = 2f;
    private float timer;

    public float distance = 3;
    
    private readonly Vector2 sizeC = new(1f, 1f);

    private void Start()
    {
        timer = timeSpawn;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (!(timer <= 0)) return;
        timer = timeSpawn;
        
        var posTile = grid.WorldToCell(Random.insideUnitCircle * distance);
        var posSpawn = new Vector3(posTile.x + 0.5f, posTile.y + 0.5f, posTile.z);
        while (CheckSpawn(posSpawn))
        {
            posTile = grid.WorldToCell(Random.insideUnitCircle * distance);
            posSpawn = new Vector3(posTile.x + 0.5f, posTile.y + 0.5f, posTile.z);
        }
        Instantiate(stone, posSpawn, Quaternion.identity, transform);
        posTile = grid.WorldToCell(Random.insideUnitCircle * distance);
        posSpawn = new Vector3(posTile.x + 0.5f, posTile.y + 0.5f, posTile.z);
        while (CheckSpawn(posTile))
        {
            posTile = grid.WorldToCell(Random.insideUnitCircle * distance);
            posSpawn = new Vector3(posTile.x + 0.5f, posTile.y + 0.5f, posTile.z);
        }
        Instantiate(tree, posSpawn, Quaternion.identity, transform);
    }

    private bool CheckSpawn(Vector3 pos)
    {
        var colliders = Physics2D.OverlapBox(pos, sizeC,0);
        return colliders;
    }
}
