using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildScript : MonoBehaviour
{
    private UpTower buildingToPlace;

    public CustomCursor cursor;

    public GameObject zone;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && buildingToPlace != null)
        {
            Instantiate(buildingToPlace, cursor.transform.position, Quaternion.identity);
            buildingToPlace = null;
            cursor.gameObject.SetActive(false);
            zone.SetActive(false);
            Cursor.visible = true;
        }

    }

    public void ConstructionBuilding(UpTower building)
    {
        cursor.gameObject.SetActive(true);
        cursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
        buildingToPlace = building;
        zone.SetActive(true);
        cursor.GetComponent<Renderer>().material.color = Color.red;
        Cursor.visible = false;
    }
}
