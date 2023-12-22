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

    public GameObject ball;
    
    public Transform attackPos;
    public float radius;
    public float damage;

    private float recharge;
    public float startRecharge;

    public GameObject nearest;

    public GameObject corpse;

    private Manager manager;

    // Start is called before the first frame update
    
    public SpriteRenderer healthBar;
    
    public SpriteRenderer backGround;

    private Animator anim;

    private bool bleeding;

    private GameObject Baza;
    
    private float timer;
    private float needTime = 0f;
    void Start()
    {
        Baza = GameObject.FindGameObjectWithTag("Baza");
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
        FindTower();
        if (nearest == null)
        {
            return;
        }
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
            anim.SetTrigger("attack");
            recharge = 0;
            
        }
        
    }

    private void FindTower()
    {
        nearest = Baza;
        if (nearest == null)
        {
            return;
        }
        var position = transform.position;
        var distance = (nearest.transform.position - position).sqrMagnitude;
        foreach (var tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            var diff = tower.transform.position - position;
            var curDistance = diff.sqrMagnitude;
            if (!(curDistance < distance)) continue;
            nearest = tower;
            distance = curDistance;
        }
    }
    
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }

    public void OnAttack()
    {
        switch (name)
        {
            case "Wizard(Clone)":
                var newBall = Instantiate(ball, transform.position, Quaternion.identity, transform);
                newBall.GetComponent<Ball>().target = nearest;
                newBall.GetComponent<Ball>().tower = gameObject;
                newBall.GetComponent<Ball>().damage = damage;
                break;
            default:
                if (nearest.gameObject.CompareTag("Baza"))
                {
                    nearest.gameObject.GetComponent<Baza>().TakeDamage(damage);
                    return;
                }
                nearest.gameObject.GetComponent<UpTower>().TakeDamage(damage);
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        healthBar.size = new Vector2(hp / maxHP, 0.14f);
        hp -= damage;
    }

    public void Bleeding(float time, float damage)
    {
        if (bleeding) return;
        bleeding = true;
        StartCoroutine(BleedDamage(time, damage));
    }
    private IEnumerator BleedDamage(float time, float damage)
    {
        var timer = 0f;
        while (timer < time)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(1f);
            timer += 1f;
        }
        bleeding = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("fire") && name != "Wisp(Clone)")
        {
            timer += Time.deltaTime;
            if (timer >= needTime)
            {
                needTime += 1f;
                TakeDamage(3);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("fire"))
        {
            timer = 0f;
            needTime = 0f;
        }
    }
}
