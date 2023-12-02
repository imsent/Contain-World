using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpTower : MonoBehaviour
{
    public int hp;
    

    public Transform attackPos;
    public LayerMask enemyMask;
    public float radius;
    public int damage;

    private float recharge;
    public float startRecharge;
    
    private Manager manager;

    public EnemyNear targetEnemy = null;

    public GameObject ball;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
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
            Attack();
        }
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
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }
    private void Attack()
    {
        var newBall = Instantiate(ball, transform.position, Quaternion.identity);
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
