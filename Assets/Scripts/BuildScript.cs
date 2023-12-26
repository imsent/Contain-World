using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class BuildScript : MonoBehaviour
{
    public Grid grid;

    private GameObject buildingToPlace;
    
    // Start is called before the first frame update
    private Player playerinv;
    
    public GameObject buildText;

    public GameObject tower;

    private readonly Vector2 sizeC = new(0.9f, 0.9f);

    private Manager manager;

    private Baza Baza;
    // Update is called once per frame
    private void Start()
    {
        Baza = GameObject.FindGameObjectWithTag("Baza").GetComponent<Baza>();
        playerinv = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Manager>();
    }

    void Update()
    {
        buildText.transform.position = new Vector3(playerinv.transform.position.x, playerinv.transform.position.y + 0.5f, playerinv.transform.position.z);
        if (!Input.GetKeyDown(KeyCode.E) || buildingToPlace == null) return;
        var postile = grid.WorldToCell(playerinv.transform.position);
        var posPlace = new Vector3(postile.x + 0.5f, postile.y + 0.5f, postile.z);
        var colliders = Physics2D.OverlapBoxAll(posPlace, sizeC,0);
        var col = colliders.FirstOrDefault(x => x.CompareTag("Tower"));
        Debug.Log(col);
        if (colliders.Length == 2 && colliders[1].CompareTag("infection"))
        {
            Instantiate(buildingToPlace, posPlace, Quaternion.identity);
            buildingToPlace = null;
            buildText.SetActive(false);
            manager.towerCountPlace += 1;
            playerinv.inv = null;
            playerinv.PlaySound(playerinv.sounds[1]);
        }else if (col != null && col.gameObject.GetComponent<UpTower>().upgradeImage.GetComponent<SpriteRenderer>().sprite != null)
        {
            var tower = col.gameObject.GetComponent<UpTower>();
            tower.hp = tower.maxHP;
            buildText.SetActive(false);
            tower.healthBar.size = new Vector2(1, 0.14f);
            manager.towerCountPlace += 1;
            playerinv.inv = null;
            playerinv.PlaySound(playerinv.sounds[1]);
        }
        else
        {
            playerinv.Error("Вы не можете тут строить");
        }
    }

    public void ConstructionBuilding()
    {
        if (playerinv.stones >= 5 && playerinv.trees >= 5)
        {
            Baza.buildText.SetActive(false);
            playerinv.inv = "build";
            buildingToPlace = tower;
            buildText.SetActive(true);
            playerinv.stones -= 5;
            playerinv.trees -= 5;
        }
        else
        {
            playerinv.Error("Не хватает ресурсов");
        }
    }

    
}
