using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baza : MonoBehaviour
{
    public int hp;

    public Result result;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            result.resultGame(false);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
}
