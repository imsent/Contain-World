using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baza : MonoBehaviour
{
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("321123");
        hp -= damage;
    }
}