using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baza : MonoBehaviour
{
    public int hp;

    public Result result;
    
    public Transform spawnPoint;

    public GameObject Error;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            result.resultGame(false);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        var go = Instantiate(Error, spawnPoint.localPosition, Quaternion.identity);
        go.transform.SetParent(spawnPoint.transform,true);
        go.GetComponent<TMPro.TextMeshPro>().SetText("-" + damage);
        go.GetComponent<TMPro.TextMeshPro>().fontSize = 2;
        go.GetComponent<TMPro.TextMeshPro>().color = Color.red;
        go.name = "no money";
        Destroy(go,0.5f);
    }
}
