using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildScript : MonoBehaviour
{
    private UpTower buildingToPlace;
    
    public GameObject zone;
    // Start is called before the first frame update
    private Player playerinv;

    public Transform spawnPoint;

    public GameObject Error;

    public GameObject buildText;

    
    // Update is called once per frame
    private void Start()
    {
        playerinv = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        buildText.transform.position = new Vector3(playerinv.transform.position.x, playerinv.transform.position.y + 0.5f, playerinv.transform.position.z);
        if (Input.GetKeyDown(KeyCode.E) && buildingToPlace != null)
        {
            if (playerinv.canBuild)
            {
                Instantiate(buildingToPlace, playerinv.transform.position, Quaternion.identity);
                buildingToPlace = null;
                //cursor.gameObject.SetActive(false);
                zone.gameObject.GetComponent<SpriteRenderer>().color =  new Color(152/255f,0,255/255f,76/255f);
                buildText.SetActive(false);
                //Cursor.visible = true;
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
    }

    public void ConstructionBuilding(UpTower building)
    {
        if (playerinv.stones >= 5 && playerinv.trees >= 5)
        {
            playerinv.canBuild = true;
            playerinv.inv = "build";
            //cursor.gameObject.SetActive(true);
            //cursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
            buildingToPlace = building;
            zone.gameObject.GetComponent<SpriteRenderer>().color =  new Color(0,255/255f,7/255f,76/255f);
            //cursor.GetComponent<Renderer>().material.color = Color.red;
            //Cursor.visible = false;
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
