using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpTower : MonoBehaviour
{
    public float hp;
    private float maxHP;
    public float protection;
    

    public Transform attackPos;
    public float radius;
    public float damage;

    private float recharge;
    public float startRecharge;
    
    private Manager manager;

    public EnemyNear targetEnemy;

    public GameObject ball;
    public int countBall;
    
    public SpriteRenderer healthBar;
    
    public SpriteRenderer backGround;

    public GameObject upText;

    private Animator anim;

    public GameObject upgradeImage;

    public bool regeneration;

    private float timeRegen;

    private float needTimeRegen = 3f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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

        Regenerate();
    }

    public void TakeDamage(float damage)
    {
        hp -= damage * (1f-protection);
        healthBar.size = new Vector2(hp / maxHP, 0.14f);
        if (!regeneration) return;
        timeRegen = 0f;
        needTimeRegen = 3f;
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
        if (targetEnemy != null && recharge >= startRecharge && Vector2.Distance(transform.position, targetEnemy.transform.position) < radius)
        {
            anim.SetTrigger("attack");
            recharge = 0;
        }

    }

    private void UpgradeTower()
    {
        if (Input.GetKeyDown(KeyCode.E) && upText.activeSelf)
        {
            manager.upgradeTower(this);
        }
    }

    private void onAttack()
    {
        for (var i = 0; i < countBall; i++)
        {
            var newBall = Instantiate(ball, transform.position, Quaternion.identity, transform);
            newBall.GetComponent<Ball>().target = targetEnemy.gameObject;
            newBall.GetComponent<Ball>().tower = gameObject;
            newBall.GetComponent<Ball>().damage = damage;
        }
    }

    private IEnumerable<EnemyNear> GetEnemiesInRange()
    {
        return manager.EnemyList
            .Where(enemy => Vector2.Distance(transform.position, enemy.transform.position) <= radius).ToList();
    }

    private EnemyNear GetNearestEnemy()
    {
        EnemyNear nearestEnemy = null;
        var smallestDistance = float.PositiveInfinity;
        foreach (var enemy in GetEnemiesInRange().Where(enemy => Vector2.Distance(transform.position, enemy.transform.position) < smallestDistance))
        {
            smallestDistance = Vector2.Distance(transform.position, enemy.transform.position);
            nearestEnemy = enemy;
        }
        return nearestEnemy;
    }

    private void Regenerate()
    {
        if (regeneration)
        {
            timeRegen += Time.deltaTime;
            if (timeRegen >= needTimeRegen)
            {
                needTimeRegen += 1f;
                hp += 5;
                healthBar.size = new Vector2(hp / maxHP, 0.14f);
                if (hp >= maxHP) hp = maxHP;
            }
        }
    }
}
