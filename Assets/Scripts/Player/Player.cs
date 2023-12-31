using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : Sounds
{
    public float speed;

    public int stones;

    public int trees;

    public float corpses;
    
    public float corpsesNeed = 20;

    public int lvl = 1;

    private GameObject UI;
    
    private GameObject res;
    private Image resUp;
    private Image resDown;
    private Text stoneCount;
    private Text treeCount;
    
    private GameObject kill;
    private Text corpseCount;
    private Text corpseNeeded;
    private Image killFill;

    private GameObject infection;
    private Text infectionText;
    private Image infectionFill;


    private Rigidbody2D rb;

    private Vector2 direction;

    private Animator anim;

    public string inv;

    public enemySpawn spawner;

    private GameObject invModel;

    public Result result;
    
    
    public GameObject[] infections;
    

    private Manager manager;

    public bool upVision;
    private Baza Baza;

    public GameObject error;

    private GameObject errorNow;
    // Start is called before the first frame update
    void Start()
    {
        Baza = GameObject.FindGameObjectWithTag("Baza").GetComponent<Baza>();
        UI = GameObject.FindGameObjectWithTag("UI");
        res = UI.transform.GetChild(0).gameObject;
        resUp = res.transform.GetChild(0).GetComponent<Image>();
        resDown = res.transform.GetChild(1).GetComponent<Image>();
        stoneCount = res.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
        treeCount = res.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
        
        manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Manager>();
        
        kill = UI.transform.GetChild(2).gameObject;
        corpseCount = kill.transform.GetChild(2).gameObject.GetComponent<Text>();
        corpseNeeded = kill.transform.GetChild(4).gameObject.GetComponent<Text>();
        killFill = kill.transform.GetChild(0).gameObject.GetComponent<Image>();
        corpseNeeded.text = corpsesNeed.ToString();
        
        Time.timeScale = 1f;
        
        invModel = transform.GetChild(1).gameObject;

        infection = UI.transform.GetChild(4).gameObject;
        infectionFill = infection.transform.GetChild(0).gameObject.GetComponent<Image>();
        infectionText = infection.transform.GetChild(1).gameObject.GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        invModel.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        treeCount.text = trees.ToString();
        stoneCount.text = stones.ToString();
        if (trees < 5 || stones < 5)
        {
            resUp.color = new Color(181 / 255f, 157 / 255f, 157 / 255f);
            resDown.color = new Color(104 / 255f, 69 / 255f, 69 / 255f);
        }
        else
        {
            resUp.color = new Color(163 / 255f, 180 / 255f, 156 / 255f);
            resDown.color = new Color(78 / 255f, 106 / 255f, 81 / 255f);
        }
        corpseCount.text = corpses.ToString();
        killFill.fillAmount = corpses / corpsesNeed;
        if (corpses == corpsesNeed)
        {
            PlaySound(sounds[3]);
            corpses = 0;
            lvl += 1;
            manager.zonePercent += 0.2f;
            infectionFill.fillAmount = manager.zonePercent / 1f;
            infectionText.text = manager.zonePercent * 100 + "% заражено";
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
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Horizontal",direction.x);
        anim.SetFloat("Vertical",direction.y);
        anim.SetFloat("Speed",direction.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
    }
    

    private void OnTriggerEnter2D(Collider2D other)
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
                    manager.countStone--;
                    stones += 1;
                    inv = null;
                    
                    break;
                case "Tree":
                    manager.countTree--;
                    trees += 1;
                    inv = null;
                    break;
            }
            return;
        }
        if (other.gameObject.CompareTag("infection")) speed += 0.6f;
        if (!other.gameObject.CompareTag("Tree") && !other.gameObject.CompareTag("Stone")) return;

        if (!string.IsNullOrEmpty(inv)) return;
        PlaySound(sounds[0]);
        invModel.GetComponent<SpriteRenderer>().sprite = other.gameObject.GetComponent<SpriteRenderer>().sprite;
        invModel.SetActive(true);
        inv = other.gameObject.tag;
        Destroy(other.gameObject.GameObject());
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tower") && !upVision && other.gameObject.GetComponent<UpTower>().upgradeImage.GetComponent<SpriteRenderer>().sprite == null && inv != "build" && !Baza.buildText.activeSelf)
        {
            upVision = true;
            other.gameObject.GetComponent<UpTower>().upText.SetActive(true);
            return;
        }
        if (other.gameObject.CompareTag("Baza") && !upVision && inv != "build") other.gameObject.GetComponent<Baza>().buildText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("infection")) speed -= 0.6f;
        if (other.gameObject.CompareTag("Tower") && upVision)
        {
            upVision = false;
            other.gameObject.GetComponent<UpTower>().upText.SetActive(false);
            return;
        }
        if (other.gameObject.CompareTag("Baza"))
        {
            other.gameObject.GetComponent<Baza>().buildText.SetActive(false);
        }
    }

    public void Error(string text)
    {
        if (errorNow != null && errorNow.GetComponent<TextMeshPro>().text == text) return; 
        errorNow = Instantiate(error, transform.GetChild(0).localPosition, Quaternion.identity);
        errorNow.transform.SetParent(transform.GetChild(0),true);
        errorNow.GetComponent<TextMeshPro>().SetText(text);
        Destroy(errorNow,0.5f);
    }
}
