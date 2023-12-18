using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;

    public int stones;

    public int trees;

    public float corpses;
    
    public float corpsesNeed = 20;

    public int lvl = 1;

    public TextMeshProUGUI stoneCount;
    
    public TextMeshProUGUI treeCount;
    
    public TextMeshProUGUI corpseCount;

    public TextMeshProUGUI corpseNeeded;

    public Image killFill;
    

    private Rigidbody2D rb;

    private Vector2 direction;

    private Animator anim;

    public string inv;
    
    
    
    public enemySpawn spawner;

    public GameObject invModel;

    public Result result;
    
    
    public GameObject[] infections;
    
    
    public int nowUpgrade;


    private Manager manager;

    private bool upVision = false;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Manager>();
        Time.timeScale = 1f;
        corpseNeeded.text = corpsesNeed.ToString();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        invModel.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        treeCount.text = trees.ToString();
        stoneCount.text = stones.ToString();
        treeCount.color = trees < 5 ? new Color(194/255f,66/255f,66/255f) : new Color(95/255f,255/255f,130/255f);
        stoneCount.color = stones < 5 ? new Color(194/255f,66/255f,66/255f) : new Color(95/255f,255/255f,130/255f);
        corpseCount.text = corpses.ToString();
        killFill.fillAmount = corpses / corpsesNeed;
        if (corpses == corpsesNeed)
        {
            corpses = 0;
            lvl += 1;
            manager.zonePercent += 0.2f;
            spawner.SelectWave(lvl);
            infections[lvl-1].SetActive(true);
            infections[lvl-2].SetActive(false);
            switch (lvl)
            {
                case 2:
                    corpsesNeed = 30;
                    break;
                case 3:
                    corpsesNeed = 40;
                    break;
                case 4:
                    corpsesNeed = 50;
                    break;
                case 5:
                    corpsesNeed = 60;
                    break;
                case 6:
                    result.resultGame(true);
                    break;
            }

            corpseNeeded.text = corpsesNeed.ToString();
        }
        direction.x = Input.GetAxisRaw(("Horizontal"));
        direction.y = Input.GetAxisRaw(("Vertical"));
        
        anim.SetFloat("Horizontal",direction.x);
        anim.SetFloat("Vertical",direction.y);
        anim.SetFloat("Speed",direction.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Baza"))
        {
            if (inv != null)
            {
                invModel.SetActive(false);
            }
            switch (inv)
            {
                case "Stone":
                    stones += 1;
                    inv = null;
                    
                    break;
                case "Tree":
                    trees += 1;
                    inv = null;
                    break;
                case "Corpse":
                    corpses += 1;
                    inv = null;
                    break;
            }

            other.gameObject.GetComponent<Baza>().buildText.SetActive(true);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Tower"))
        //{
        //    other.gameObject.GetComponent<UpTower>().upText.SetActive(true);
        //}
        
        if ((!other.gameObject.CompareTag("Tree") && !other.gameObject.CompareTag("Stone") &&
             !other.gameObject.CompareTag("Corpse"))) return;

        if (!string.IsNullOrEmpty(inv)) return;
        
        invModel.GetComponent<SpriteRenderer>().sprite = other.gameObject.GetComponent<SpriteRenderer>().sprite;
        invModel.SetActive(true);
        inv = other.gameObject.tag;
        Destroy(other.gameObject.GameObject());
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Baza"))
        {
            other.gameObject.GetComponent<Baza>().buildText.SetActive(false);
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tower") && !upVision)
        {
            upVision = true;
            other.gameObject.GetComponent<UpTower>().upText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tower") && upVision)
        {
            upVision = false;
            other.gameObject.GetComponent<UpTower>().upText.SetActive(false);
        }
    }
}
