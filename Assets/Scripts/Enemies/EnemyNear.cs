using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyNear : MonoBehaviour
{
    public float speed;
    public int hp;

    private Transform baza;

    public Transform attackPos;
    public LayerMask TowerMask;
    public float radius;
    public int damage;

    private float recharge;
    public float startRecharge;
    // Start is called before the first frame update
    
    void Start()
    {
        baza = GameObject.FindGameObjectWithTag("Baza").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, baza.position, speed * Time.deltaTime);
        recharge += Time.deltaTime;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (recharge >= startRecharge)
        {
            if (other.CompareTag("Baza"))
            {
                OnAttack();
                recharge = 0;
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }

    public void OnAttack()
    {
        Collider2D[] tower = Physics2D.OverlapCircleAll(attackPos.position, radius, TowerMask);
        foreach (var i in tower)
        {
            i.gameObject.GetComponent<Baza>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
    
}
