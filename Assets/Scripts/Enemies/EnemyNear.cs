using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class EnemyNear : MonoBehaviour
{
    public float speed;
    public float hp;
    private float maxHP;
    private Transform baza;

    public Transform attackPos;
    public LayerMask TowerMask;
    public float radius;
    public int damage;

    private float recharge;
    public float startRecharge;

    
    private GameObject closest;

    public GameObject nearest;

    public GameObject corpse;

    private Manager manager;
    
    // Start is called before the first frame update
    
    public SpriteRenderer healthBar;
    
    public SpriteRenderer backGround;

    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        maxHP = hp;
        manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Manager>();
        manager.AddEnemy(this);
        healthBar.size = new Vector2(1.02f, 0.14f);
        backGround.size = new Vector2(1.02f, 0.14f);
    }

    // Update is called once per frame
    void Update()
    {
        recharge += Time.deltaTime;
        if (hp <= 0)
        {
            Instantiate(corpse, transform.position, quaternion.identity);
            manager.RemoveEnemy(this);
            manager.maxEnemy--;
            manager.kills += 1;
            Destroy(gameObject);
        }
        nearest = FindTower();
        anim.SetFloat("swap", Convert.ToSingle(nearest.transform.position.x < transform.position.x));
        if (Vector2.Distance(transform.position, nearest.transform.position) > radius)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, nearest.transform.position, speed * Time.deltaTime);
            anim.SetBool("move", true);
        }
        else
        {
            anim.SetBool("move", false);
            if (!(recharge >= startRecharge)) return;
            OnAttack();
            recharge = 0;
            
        }
        
    }

    private GameObject FindTower()
    {
        closest = GameObject.FindGameObjectWithTag("Baza");
        var position = transform.position;
        var distance = (closest.transform.position - position).sqrMagnitude;
        foreach (var tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            var diff = tower.transform.position - position;
            var curDistance = diff.sqrMagnitude;
            if (!(curDistance < distance)) continue;
            closest = tower;
            distance = curDistance;
        }
        return closest;
    }
    
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }

    public void OnAttack()
    {
        anim.SetBool("attack",true);
        var tower = Physics2D.OverlapCircleAll(attackPos.position, radius, TowerMask);
        foreach (var i in tower)
        {
            if (i.gameObject.CompareTag("Baza"))
            {
                i.gameObject.GetComponent<Baza>().TakeDamage(damage);
                return;
            }
            i.gameObject.GetComponent<UpTower>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        healthBar.size = new Vector2(hp / maxHP, 0.14f);
        hp -= damage;
    }
    
}
