using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpTower : MonoBehaviour
{
    public float hp;
    private float maxHP;
    

    public Transform attackPos;
    public float radius;
    public int damage;

    private float recharge;
    public float startRecharge;
    
    private Manager manager;

    public EnemyNear targetEnemy = null;

    public GameObject ball;
    
    public SpriteRenderer healthBar;
    
    public SpriteRenderer backGround;

    public GameObject upText;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Manager>();
        maxHP = hp;
        healthBar.size = new Vector2(1.02f, 0.14f);
        backGround.size = new Vector2(1.02f, 0.14f);
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        UpgradeTower();
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        
    }
    
    public void TakeDamage(int damage)
    {
        hp -= damage;
        healthBar.size = new Vector2(hp / maxHP, 0.14f);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }
    private void Attack()
    {
        if (targetEnemy == null)
        {
            targetEnemy = GetNearestEnemy();
        }

        if (targetEnemy != null && Vector2.Distance(transform.position, targetEnemy.transform.position) > radius)
        {
            targetEnemy = null;
        }
        recharge += Time.deltaTime;
        if (recharge >= startRecharge)
        {
            recharge = 0;
            onAttack();
        }

    }

    private void UpgradeTower()
    {
        if (Input.GetKeyDown(KeyCode.E) && upText.activeSelf)
        {
            return;
        }

    }

    public void onAttack()
    {
        var newBall = Instantiate(ball, transform.position, Quaternion.identity, transform);
        newBall.GetComponent<Ball>().target = targetEnemy;
        newBall.GetComponent<Ball>().tower = this;
    }
    public List<EnemyNear> GetEnemiesInRange()
    {
        return manager.EnemyList
            .Where(enemy => Vector2.Distance(transform.position, enemy.transform.position) <= radius).ToList();
    }

    public EnemyNear GetNearestEnemy()
    {
        EnemyNear nearestEnemy = null;
        var smallestDistance = float.PositiveInfinity;
        foreach (var enemy in GetEnemiesInRange())
        {
            if (Vector2.Distance(transform.position, enemy.transform.position) < smallestDistance)
            {
                smallestDistance = Vector2.Distance(transform.position, enemy.transform.position);
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
