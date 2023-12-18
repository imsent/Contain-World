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

    public Transform spawnPoint;

    public GameObject Error;

    public GameObject buildText;

    public GameObject tower;

    private readonly Vector2 sizeC = new(0.9f, 0.9f);

    private Manager manager;
    // Update is called once per frame
    private void Start()
    {
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
        if (colliders.Length == 2 && colliders[1].CompareTag("infection"))
        {
            Instantiate(buildingToPlace, posPlace, Quaternion.identity);
            buildingToPlace = null;
            buildText.SetActive(false);
            manager.towerCountPlace += 1;
            playerinv.inv = null;
        }
        else
        {
            var go = Instantiate(Error, spawnPoint.localPosition, Quaternion.identity);
            go.transform.SetParent(spawnPoint.transform,true);
            go.GetComponent<TMPro.TextMeshPro>().SetText("Вы не можете тут строить");
            go.GetComponent<TMPro.TextMeshPro>().fontSize = 2;
            go.GetComponent<TMPro.TextMeshPro>().color = Color.red;
            go.name = "cantbuild";
            Destroy(go,0.5f);
        }
    }

    public void ConstructionBuilding()
    {
        if (playerinv.stones >= 5 && playerinv.trees >= 5)
        {
            playerinv.inv = "build";
            buildingToPlace = tower;
            buildText.SetActive(true);
            playerinv.stones -= 5;
            playerinv.trees -= 5;
        }
        else
        {
            var go = Instantiate(Error, spawnPoint.localPosition, Quaternion.identity);
            go.transform.SetParent(spawnPoint.transform,true);
            go.GetComponent<TMPro.TextMeshPro>().SetText("Не хватает ресурсов");
            go.GetComponent<TMPro.TextMeshPro>().fontSize = 2;
            go.GetComponent<TMPro.TextMeshPro>().color = Color.red;
            go.name = "no money";
            Destroy(go,0.5f);
        }
    }

    
}
