using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    public float speed;

    private Vector2 baza;
    // Start is called before the first frame update
    void Start()
    {
        baza = new Vector2(-0.5f, -0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, baza, speed * Time.deltaTime);
        if (!(Vector2.Distance(transform.position, baza) <= 0.5f)) return;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().corpses += 1;
        Destroy(gameObject);
    }
}
