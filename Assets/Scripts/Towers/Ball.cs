using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;

    public float damage;
    
    public GameObject target;

    public GameObject tower;

    
    public GameObject spawnObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }else
        {
            var dir = target.transform.position - tower.transform.position;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            transform.position =
                Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject == target)
            {
                switch (other.gameObject.name)
                {
                    case "Wizard(Clone)":
                        switch (name)
                        {
                            case "iceBall(Clone)":
                                damage *= 1.5f;
                                break;
                            case "fireBall(Clone)":
                                damage *= 0.5f;
                                break;
                        }
                        break;
                    case "Golem(Clone)":
                        switch (name)
                        {
                            case "iceBall(Clone)":
                                damage *= 0.5f;
                                break;
                            case "fireBall(Clone)":
                                damage *= 1.5f;
                                break;
                        }
                        break;
                    case "Wisp(Clone)":
                        damage *= name == "chaosBall(Clone)" ? 1.5f : 0.85f;
                        break;
                    
                }
                switch (name)
                {
                    case "fireBall(Clone)":
                        if (Random.Range(1, 100) < 30)
                        {
                            var postile = GameObject.FindGameObjectWithTag("grid").GetComponent<Grid>().WorldToCell(other.transform.position);
                            var posPlace = new Vector3(postile.x + 0.5f, postile.y + 0.5f, postile.z);
                            Instantiate(spawnObj, posPlace, Quaternion.identity);
                        }
                        break;
                    case "iceBall(Clone)":
                        other.gameObject.GetComponent<Enemy>().speed *= 0.9f;
                        break;
                    case "chaosBall(Clone)":
                        damage = Random.Range(2, 10);
                        break;
                    case "poisonBall(Clone)":
                        other.gameObject.GetComponent<Enemy>().Bleeding(3f, 5f);
                        break;
                }
                other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }else if (other.gameObject.CompareTag("Tower"))
        {
            if (other.gameObject == target)
            {
                other.gameObject.GetComponent<UpTower>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }else if (other.gameObject.CompareTag("Baza"))
        {
            if (other.gameObject == target)
            {
                other.gameObject.GetComponent<Baza>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
