using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baza : MonoBehaviour
{
    public float hp;

    private float maxHP;

    public Result result;

    public GameObject buildText;

    public BuildScript buildScript;

    private SpriteRenderer healthBar;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = hp;
        healthBar = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && buildText.activeSelf)
        {
            buildScript.ConstructionBuilding();
        }
        if (hp <= 0)
        {
            result.resultGame(false);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        healthBar.size = new Vector2(1.4375f, 1.5f*(hp/maxHP));
    }
    
}
