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

    public GameObject stoneCount;
    
    public GameObject treeCount;

    private Rigidbody2D rb;

    private Vector2 direction;

    private Animator anim;

    public string inv;

    
    public GameObject buildButton;

    public GameObject upButton1;

    public GameObject upButton2;

    public GameObject nextTower;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        treeCount.GetComponent<TMPro.TextMeshProUGUI>().text = trees.ToString();
        stoneCount.GetComponent<TMPro.TextMeshProUGUI>().text = stones.ToString();
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
            }

            buildButton.gameObject.SetActive(true);
            buildButton.transform.position = Camera.main.WorldToScreenPoint(other.transform.position);
        }

        if (other.gameObject.CompareTag("Tower"))
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
        if ((other.gameObject.CompareTag("Tree") || other.gameObject.CompareTag("Stone")) &&
            string.IsNullOrEmpty(inv))
        {
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
        if (other.gameObject.CompareTag("Tower"))
        {
            upButton1.gameObject.SetActive(false);
            upButton2.gameObject.SetActive(false);
        }
    }
}
