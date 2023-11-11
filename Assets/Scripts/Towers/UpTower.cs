using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            if (other.CompareTag("Enemy"))
            {
                OnAttack();
                recharge = 0;
            }
        }
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
    public void OnAttack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPos.position, radius, enemyMask);
        foreach (var i in enemy)
        {
            i.GetComponent<EnemyNear>().TakeDamage(damage);
        }
    }
}
