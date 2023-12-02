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

    public GameObject zone;

    public TextMeshProUGUI stoneCount;
    
    public TextMeshProUGUI treeCount;
    
    public TextMeshProUGUI corpseCount;

    public TextMeshProUGUI corpseNeeded;

    public Image killFill;
    

    private Rigidbody2D rb;

    private Vector2 direction;

    private Animator anim;

    public string inv;

    
    public GameObject buildButton;

    public GameObject upButton1;

    public GameObject upButton2;

    public GameObject nextTower;
    
    public bool canBuild;

    public enemySpawn spawner;

    public GameObject invModel;

    public Result result;
    // Start is called before the first frame update
    void Start()
    {
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
            zone.transform.localScale += new Vector3(10.5f, 8.4f, 0);
            lvl += 1;
            spawner.SelectWave(lvl);
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
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
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

            buildButton.gameObject.SetActive(true);
            buildButton.transform.position = Camera.main.WorldToScreenPoint(other.transform.position);
        }

        if (other.gameObject.CompareTag("Tower") && false)
        {
            upButton1.gameObject.SetActive(true);
            upButton1.gameObject.GetComponent<upLvlTower>().towerNow = other.gameObject;
            upButton1.gameObject.GetComponent<upLvlTower>().towerNext = nextTower;
            upButton1.transform.position = Camera.main.WorldToScreenPoint(other.transform.position);
            upButton1.transform.localPosition = new Vector2(upButton1.transform.localPosition.x - 50,upButton1.transform.localPosition.y);
            upButton2.gameObject.SetActive(true);
            upButton2.transform.position = Camera.main.WorldToScreenPoint(other.transform.position);
            upButton2.transform.localPosition = new Vector2(upButton2.transform.localPosition.x + 50,upButton2.transform.localPosition.y);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("buildZone"))
        {
            canBuild = true;
        }

        if (other.gameObject.CompareTag("Tower") && inv == "build")
        {
            canBuild = false;
            return;
        }
        if ((!other.gameObject.CompareTag("Tree") && !other.gameObject.CompareTag("Stone") &&
             !other.gameObject.CompareTag("Corpse"))) return;
        if (inv == "build")
        {
            canBuild = false;
            return;
        }if (string.IsNullOrEmpty(inv))
        {
            invModel.GetComponent<SpriteRenderer>().sprite = other.gameObject.GetComponent<SpriteRenderer>().sprite;
            invModel.SetActive(true);
            inv = other.gameObject.tag;
            Destroy(other.gameObject.GameObject());
        }
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Baza"))
        {
            buildButton.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Tower") && false)
        {
            upButton1.gameObject.SetActive(false);
            upButton2.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Tree") || other.gameObject.CompareTag("Stone") ||
             other.gameObject.CompareTag("Corpse") || other.gameObject.CompareTag("Tower")) && inv == "build")
        {
            canBuild = true;
        }
        if (other.gameObject.CompareTag("buildZone"))
        {
            canBuild = false;
        }
    }
}
